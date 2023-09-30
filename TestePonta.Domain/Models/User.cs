using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestePonta.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        // Adicione quaisquer outros campos relevantes para o usuário

        public ICollection<Task> Tasks { get; set; } // Se desejar associar usuários a tarefas
    }
}
