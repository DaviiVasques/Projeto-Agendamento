using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Data;
using SistemaAgendamento.Models;
using System.Transactions;

namespace SistemaAgendamento.Controllers
{
    [Route("api/agendamentos")]
    [ApiController]
    public class ApiAgendamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApiAgendamentosController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/agendamentos
        [HttpPost]
        public async Task<IActionResult> CriarAgendamento([FromBody] AgendamentoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar se a disponibilidade existe e está disponível
            var disponibilidade = await _context.Disponibilidades
                .FirstOrDefaultAsync(d => d.Id == request.DisponibilidadeId && d.Disponivel);

            if (disponibilidade == null)
            {
                return BadRequest("Disponibilidade não encontrada ou não disponível.");
            }

            // Verificar se o horário do agendamento está dentro da disponibilidade
            if (request.DataHora < disponibilidade.DataHoraInicio || request.DataHora >= disponibilidade.DataHoraFim)
            {
                return BadRequest("Horário fora da disponibilidade.");
            }

            // Verificar se já existe agendamento no mesmo horário para o profissional
            var conflito = await _context.Agendamentos
                .AnyAsync(a => a.ProfissionalId == disponibilidade.ProfissionalId &&
                               a.DataHora == request.DataHora &&
                               a.Status != "Cancelado");

            if (conflito)
            {
                return BadRequest("Horário já agendado.");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Criar ou obter cliente
                    var cliente = await _context.Clientes
                        .FirstOrDefaultAsync(c => c.Email == request.ClienteEmail);

                    if (cliente == null)
                    {
                        cliente = new Cliente
                        {
                            Nome = request.ClienteNome,
                            Email = request.ClienteEmail,
                            Telefone = request.ClienteTelefone
                        };
                        _context.Clientes.Add(cliente);
                        await _context.SaveChangesAsync();
                    }

                    // Criar agendamento
                    var agendamento = new Agendamento
                    {
                        ClienteId = cliente.Id,
                        ServicoId = request.ServicoId,
                        ProfissionalId = disponibilidade.ProfissionalId,
                        DataHora = request.DataHora,
                        Status = "Agendado"
                    };

                    _context.Agendamentos.Add(agendamento);

                    // Marcar disponibilidade como indisponível
                    disponibilidade.Disponivel = false;

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // Notificar webhook (simulado)
                    await NotificarWebhook(agendamento);

                    return CreatedAtAction(nameof(CriarAgendamento), new { id = agendamento.Id }, agendamento);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        private async Task NotificarWebhook(Agendamento agendamento)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var webhookUrl = "http://localhost:8080/WebhookPHP/NotificacaoAgendamento.php"; // Ajuste conforme necessário
                    var payload = new
                    {
                        id = agendamento.Id,
                        clienteId = agendamento.ClienteId,
                        servicoId = agendamento.ServicoId,
                        profissionalId = agendamento.ProfissionalId,
                        dataHora = agendamento.DataHora,
                        status = agendamento.Status
                    };

                    var json = System.Text.Json.JsonSerializer.Serialize(payload);
                    var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(webhookUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Webhook notificado com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine($"Erro ao notificar webhook: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao notificar webhook: {ex.Message}");
            }
        }
    }

    public class AgendamentoRequest
    {
        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteTelefone { get; set; }
        public int ServicoId { get; set; }
        public int DisponibilidadeId { get; set; }
        public DateTime DataHora { get; set; }
    }
}
