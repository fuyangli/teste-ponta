using TestePonta.Core.DTOs;
using TaskStatus = TestePonta.Core.Enums.TaskStatus;

namespace TestePonta.Domain.Services
{
    public interface ITaskService
    {
        Task<TaskDTO> CreateTaskAsync(TaskDTO task);
        Task<TaskDTO> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskDTO>> GetAllTasksAsync();
        Task<IEnumerable<TaskDTO>> GetTasksByStatusAsync(TaskStatus status);
        Task<TaskDTO> UpdateTaskAsync(TaskDTO task);
        Task<bool> DeleteTaskAsync(int id);
    }
}
