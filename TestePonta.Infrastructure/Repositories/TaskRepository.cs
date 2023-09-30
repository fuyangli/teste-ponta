using Microsoft.EntityFrameworkCore;
using TestePonta.Domain.Repositories;
using TestePonta.Infrastructure.DbContexts;

using TaskStatus = TestePonta.Core.Enums.TaskStatus;

namespace TestePonta.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _taskDbContext;

        public TaskRepository(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }

        public async Task<Domain.Models.Task> AddAsync(Domain.Models.Task task)
        {
            await _taskDbContext.AddAsync(task);
            await _taskDbContext.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _taskDbContext.Tasks.FindAsync(id);
            if (entity == null) return false;

            _taskDbContext.Remove(entity);
            await _taskDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Domain.Models.Task> GetAsync(int id)
        {
            return await _taskDbContext.Tasks.Include(x => x.User).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Domain.Models.Task>> GetAllAsync()
        {
            return await _taskDbContext.Tasks.Include(x => x.User).ToListAsync();
        }

        public async Task<IEnumerable<Domain.Models.Task>> GetAllByStatusAsync(TaskStatus taskStatus)
        {
            return await _taskDbContext.Tasks.Include(x => x.User).Where(t => t.Status == taskStatus).ToListAsync();
        }

        public async Task<Domain.Models.Task> UpdateAsync(Domain.Models.Task task)
        {
            _taskDbContext.Update(task);
            await _taskDbContext.SaveChangesAsync();
            return task;
        }
    }
}
