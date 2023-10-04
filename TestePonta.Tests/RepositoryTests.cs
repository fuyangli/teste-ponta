using Microsoft.EntityFrameworkCore;
using TestePonta.Domain.Models;
using TestePonta.Domain.Repositories;
using TestePonta.Infrastructure.DbContexts;
using TestePonta.Infrastructure.Repositories;

namespace TestePonta.Tests
{
    public class REpositoryTests
    {

        private IUserRepository _userRepository;

        [SetUp]
        public void Setup()
        {
            _userRepository = new UserRepository(new TaskDbContext());
        }


        [Test]
        public async System.Threading.Tasks.Task ShouldAddUser()
        {
            var initialCount = (await _userRepository.GetAllAsync()).Count();
            var user = new User { Name = "test", Email = "teste@teste.com.br", PasswordHash = "#", PasswordSalt = "#" };
            await _userRepository.AddAsync(user);
            var actual = (await _userRepository.GetAllAsync()).Count();

            Assert.IsTrue(actual > initialCount);
        }

        [Test]
        public async System.Threading.Tasks.Task ShouldThrowDbUpdateException()
        {
            var initialCount = (await _userRepository.GetAllAsync()).Count();
            var user = new User { Name = "test2", Email = "teste2@teste.com.br" };
            

            Assert.CatchAsync<DbUpdateException>(async() => await _userRepository.AddAsync(user));
        }

    }
}