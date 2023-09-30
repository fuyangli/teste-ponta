using System.ComponentModel.DataAnnotations;
using TaskStatus = TestePonta.Core.Enums.TaskStatus;

namespace TestePonta.Domain.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public TaskStatus Status { get; set; }

        public int UserId { get; set; } // Chave estrangeira

        public User User { get; set; } // Objeto de navegação
    }

}
