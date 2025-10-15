using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefone { get; set; }

        // Relacionamento com Agendamentos
        public ICollection<Agendamento> Agendamentos { get; set; }
    }
}
