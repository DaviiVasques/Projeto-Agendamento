using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Models
{
    public class Evento
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime Data { get; set; }

        public string Descricao { get; set; }
    }
}
