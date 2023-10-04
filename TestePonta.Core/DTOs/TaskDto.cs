using System.ComponentModel.DataAnnotations;
using TaskStatus = TestePonta.Core.Enums.TaskStatus;

namespace TestePonta.Core.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) que representa uma tarefa.
    /// </summary>
    public class TaskDTO
    {
        /// <summary>
        /// Obtém ou define o ID da tarefa.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém ou define o título da tarefa.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Obtém ou define a descrição da tarefa.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Obtém ou define o status da tarefa.
        /// </summary>
        public TaskStatus Status { get; set; } = TaskStatus.Pending;

        /// <summary>
        /// Obtém ou define a data de criação da tarefa.
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Obtém ou define o ID do usuário associado à tarefa.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Obtém ou define o nome do usuário associado à tarefa.
        /// </summary>
        public string? UserName { get; set; }
    }

}
