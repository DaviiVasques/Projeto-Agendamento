using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Models
{
    public class Disponibilidade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProfissionalId { get; set; }

        [Required]
        public DateTime DataHoraInicio { get; set; }

        [Required]
        public DateTime DataHoraFim { get; set; }

        [Required]
        public bool Disponivel { get; set; } = true;

        // Relacionamento com Profissional
        public Profissional Profissional { get; set; }
    }
}
