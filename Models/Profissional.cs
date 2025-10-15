using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Models
{
    public class Profissional
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Especialidade { get; set; }

        // Relacionamento com Disponibilidades
        public ICollection<Disponibilidade> Disponibilidades { get; set; }

        // Relacionamento com Agendamentos
        public ICollection<Agendamento> Agendamentos { get; set; }
    }
}
