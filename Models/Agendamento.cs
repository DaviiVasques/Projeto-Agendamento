using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int ServicoId { get; set; }

        [Required]
        public int ProfissionalId { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Agendado"; // Agendado, Confirmado, Cancelado, Concluido

        // Relacionamentos
        public Cliente Cliente { get; set; }
        public Servico Servico { get; set; }
        public Profissional Profissional { get; set; }
    }
}
