using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Data;

namespace SistemaAgendamento.Controllers
{
    [Route("api/servicos")]
    [ApiController]
    public class ApiServicosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApiServicosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/servicos
        [HttpGet]
        public async Task<IActionResult> GetServicos()
        {
            var servicos = await _context.Servicos
                .Select(s => new
                {
                    s.Id,
                    s.Nome,
                    s.Descricao,
                    s.Duracao
                })
                .ToListAsync();

            return Ok(servicos);
        }
    }
}
