// UserRepository.cs
using Microsoft.EntityFrameworkCore;
using TestePonta.Domain.Models;
using TestePonta.Domain.Repositories;
using TestePonta.Infrastructure.DbContexts;

namespace TestePonta.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskDbContext _taskDbContext;

        public UserRepository(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;
        }

        public async Task<User> AddAsync(User user)
        {
            await _taskDbContext.Users.AddAsync(user);
            await _taskDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _taskDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return false;

            _taskDbContext.Users.Remove(user);
            await _taskDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _taskDbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _taskDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _taskDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> UpdateAsync(User user)
        {
            _taskDbContext.Users.Update(user);
            await _taskDbContext.SaveChangesAsync();
            return user;
        }
    }
}
