using TestePonta.Core.DTOs;

namespace TestePonta.Domain.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateUserAsync(UserDTO user);
        Task<UserDTO> GetUserByIdAsync(int id);
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> UpdateUserAsync(UserDTO user);
        Task<bool> DeleteUserAsync(int id);
        Task<UserDTO> AuthorizeUser(string email, string password);
    }
}
