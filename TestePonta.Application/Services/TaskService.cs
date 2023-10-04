using TestePonta.Core.DTOs;
using TestePonta.Domain.Repositories;
using TestePonta.Domain.Services;
using TaskStatus = TestePonta.Core.Enums.TaskStatus;

namespace TestePonta.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }

        public async Task<TaskDTO> CreateTaskAsync(TaskDTO taskDTO)
        {
            if (taskDTO == null)
                throw new ArgumentNullException(nameof(taskDTO));

            var task = ToTask(taskDTO);

            var addedTask = await _taskRepository.AddAsync(task);

            return new TaskDTO
            {
                Id = addedTask.Id,
                Title = addedTask.Title,
                Description = addedTask.Description,
                CreatedAt = addedTask.CreatedAt,
                Status = addedTask.Status,
                UserId = task.UserId,
            };
        }

        public async Task<TaskDTO> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetAsync(id);

            if (task == null)
                return null;

            return ToTaskDTO(task);
        }

        public async Task<IEnumerable<TaskDTO>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();

            return tasks.Select(task => ToTaskDTO(task));
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByStatusAsync(TaskStatus status)
        {
            var tasks = await _taskRepository.GetAllByStatusAsync(status);

            return tasks.Select(task => ToTaskDTO(task));
        }

        public async Task<TaskDTO> UpdateTaskAsync(TaskDTO taskDTO)
        {
            if (taskDTO == null)
                throw new ArgumentNullException(nameof(taskDTO));

            var existingTask = await _taskRepository.GetAsync(taskDTO.Id);

            if (existingTask == null)
                return null;

            existingTask.Title = taskDTO.Title;
            existingTask.Description = taskDTO.Description;
            existingTask.Status = taskDTO.Status;

            var updatedTask = await _taskRepository.UpdateAsync(existingTask);

            return ToTaskDTO(updatedTask);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetAsync(id);

            if (task == null)
                return false;

            var result = await _taskRepository.DeleteAsync(id);

            return result;
        }

        private TaskDTO ToTaskDTO(Domain.Models.Task task)
        {
            return new TaskDTO
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt,
                Status = task.Status,
                UserId = task.UserId,
                UserName = task.User.Name
            };
        }

        private Domain.Models.Task ToTask(TaskDTO taskDTO)
        {
            return new Domain.Models.Task
            {
                Title = taskDTO.Title,
                Description = taskDTO.Description,
                CreatedAt = DateTime.Now,
                Status = TaskStatus.Pending,
                UserId = taskDTO.UserId ?? -1
            };
        }
    }
}