using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Data;
using SistemaAgendamento.Models;

namespace SistemaAgendamento.Controllers
{
    [Route("api/eventos")]
    [ApiController]
    public class ApiEventosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApiEventosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/eventos
        [HttpGet]
        public async Task<IActionResult> GetEventos()
        {
            var eventos = await _context.Eventos
                .Select(e => new
                {
                    e.Id,
                    e.Nome,
                    e.Data,
                    e.Descricao
                })
                .ToListAsync();

            return Ok(eventos);
        }

        // POST: api/eventos
        [HttpPost]
        public async Task<IActionResult> CriarEvento([FromBody] Evento evento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventos), new { id = evento.Id }, evento);
        }

        // DELETE: api/eventos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarEvento(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
