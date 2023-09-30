using Microsoft.EntityFrameworkCore;
using TestePonta.Domain.Models;
using Task = TestePonta.Domain.Models.Task;

namespace TestePonta.Infrastructure.DbContexts
{
    public class TaskDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("TaskDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().HasKey(t => t.Id);
            modelBuilder.Entity<Task>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Task>().Property(t => t.Title).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Task>().Property(t => t.Description).HasMaxLength(1000);
            modelBuilder.Entity<Task>().Property(t => t.Status).IsRequired();

            modelBuilder.Entity<User>().HasKey(t => t.Id);
            modelBuilder.Entity<User>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(t => t.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<User>().Property(t => t.Email).IsRequired().HasMaxLength(1000);
            modelBuilder.Entity<User>().Property(t => t.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.PasswordSalt).IsRequired();
            modelBuilder.Entity<User>().HasMany(u => u.Tasks)
             .WithOne(t => t.User)
             .HasForeignKey(t => t.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        }

        public void InitializeData()
        {
            var users = new[]
            {
                 new User { Name = "Paulo Dias", Email = "paulodias@ponta.com.br"},
                 new User { Name = "Camila Oliveira", Email = "camilaoliveira@ponta.com.br"},
                 new User { Name = "Dev1", Email = "dev1@ponta.com.br"},
            };

            foreach(var user in users)
            {
                var password = "senha";
                var salt = PasswordHasher.GenerateSalt();
                var passwordHash = PasswordHasher.HashPassword(password, salt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = salt;
                Users.Add(user);
            }
            
            SaveChanges();
        }
    }
}
