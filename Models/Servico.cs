using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Models
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(500)]
        public string Descricao { get; set; }

        [Required]
        public int Duracao { get; set; } // em minutos

        // Relacionamento com Agendamentos
        public ICollection<Agendamento> Agendamentos { get; set; }
    }
}
