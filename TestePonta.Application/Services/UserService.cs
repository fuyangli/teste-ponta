using TestePonta.Core.DTOs;
using TestePonta.Domain.Models;
using TestePonta.Domain.Repositories;

namespace TestePonta.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO user)
        {
            var userExist = await GetUserByEmailAsync(user.Email);
            if (userExist != null)
            {
                return null;
            }
            var hash = PasswordHasher.GenerateSalt();
            var hashedPassword = PasswordHasher.HashPassword(user.Password, hash);

            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                PasswordHash = hashedPassword,
                PasswordSalt = hash
                // Outras propriedades do usuário
            };

            var createdUser = await _userRepository.AddAsync(newUser);

            // Transforme a entidade de usuário em DTO antes de retornar.
            return new UserDTO
            {
                Id = createdUser.Id,
                Name = createdUser.Name,
                Email = createdUser.Email
            };
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            // Transforme a entidade de usuário em DTO antes de retornar.
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            // Transforme a entidade de usuário em DTO antes de retornar.
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.PasswordHash
            };
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            // Transforme as entidades de usuário em DTOs antes de retornar.
            return users.Select(user => new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            });
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO user)
        {
            // Valide os dados do usuário aqui, se necessário.

            var existingUser = await _userRepository.GetByIdAsync(user.Id);

            if (existingUser == null)
            {
                return null;
            }

            // Atualize as propriedades do usuário existente com os valores do DTO.
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            // Atualize outras propriedades conforme necessário.

            var updatedUser = await _userRepository.UpdateAsync(existingUser);

            // Transforme a entidade de usuário atualizada em DTO antes de retornar.
            return new UserDTO
            {
                Id = updatedUser.Id,
                Name = updatedUser.Name,
                Email = updatedUser.Email,
                // Outras propriedades do DTO
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
            {
                return false;
            }

            var deleted = await _userRepository.DeleteAsync(id);
            return deleted;
        }

        public async Task<UserDTO> AuthorizeUser(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            if (!PasswordHasher.VerifyPassword(user.PasswordHash, password, user.PasswordSalt))
            {
                return null;
            }

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }
    }
}
