using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Data;
using SistemaAgendamento.Models;

namespace SistemaAgendamento.Controllers
{
    [Route("api/disponibilidade")]
    [ApiController]
    public class ApiDisponibilidadeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApiDisponibilidadeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/disponibilidade?data=2023-10-01
        [HttpGet]
        public async Task<IActionResult> GetDisponibilidade([FromQuery] string data)
        {
            if (!DateTime.TryParse(data, out DateTime dataConsulta))
            {
                return BadRequest("Data inválida. Use o formato YYYY-MM-DD.");
            }

            var disponibilidade = await _context.Disponibilidades
                .Include(d => d.Profissional)
                .Where(d => d.DataHoraInicio.Date == dataConsulta.Date && d.Disponivel)
                .Select(d => new
                {
                    d.Id,
                    Profissional = d.Profissional.Nome,
                    Especialidade = d.Profissional.Especialidade,
                    DataHoraInicio = d.DataHoraInicio,
                    DataHoraFim = d.DataHoraFim
                })
                .ToListAsync();

            return Ok(disponibilidade);
        }
    }
}
