using TaskStatus = TestePonta.Core.Enums.TaskStatus;

namespace TestePonta.Domain.Repositories
{
    public interface ITaskRepository
    {
        public Task<Models.Task> AddAsync(Models.Task task);

        public Task<Models.Task> UpdateAsync(Models.Task task);

        public Task<bool> DeleteAsync(int id);

        public Task<IEnumerable<Models.Task>> GetAllAsync();

        public Task<IEnumerable<Models.Task>> GetAllByStatusAsync(TaskStatus taskStatus);

        public Task<Models.Task> GetAsync(int id);


    }
}
