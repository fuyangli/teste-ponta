using System.ComponentModel.DataAnnotations;
using TaskStatus = TestePonta.Core.Enums.TaskStatus;

namespace TestePonta.Core.DTOs
{
    public class TaskDTO
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}
