using TestePonta.Infrastructure.DbContexts;

namespace TestePonta.Tests
{
    public class DbTests
    {

        private TaskDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _dbContext = new TaskDbContext();
        }


        [Test]
        public void ShouldHave0UsersWithoutInitializeData()
        {
            var userCount = _dbContext.Users.Count();
            Assert.That(userCount, Is.EqualTo(0));
        }


        [Test]
        public void ShouldHave0Tasks()
        {
            var taskCount = _dbContext.Tasks.Count();
            Assert.That(taskCount, Is.EqualTo(0));
        }

        [Test]
        public void ShouldHave3UsersOnInitializeData()
        {
            _dbContext.InitializeData();
            var userCount = _dbContext.Users.Count();
            Assert.That(userCount, Is.EqualTo(3));
        }

    }
}