using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Security.Claims;
using TestePonta.Core.DTOs;
using TestePonta.Domain.Models;
using TestePonta.Domain.Services;

namespace TestePonta.Controllers
{
    /// <summary>
    /// Controller para gerenciar operações de usuário.
    /// </summary>
    [Route("api/users")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IUserService _userService;

        /// <summary>
        /// Construtor da UserController.
        /// </summary>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtém todos os usuários.
        /// </summary>
        /// <returns>Uma lista de usuários.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            _logger.Trace("Pedido de listagem de Usuários");
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Obtém as informações do usuário autenticado.
        /// </summary>
        /// <remarks>Requer autenticação.</remarks>
        /// <returns>As informações do usuário autenticado.</returns>
        [HttpGet("logged")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetLogged()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetUserByIdAsync(int.Parse(userId));
            if (user == null)
            {
                _logger.Trace("Não encontrou usuário {0}", userId);
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">O ID do usuário a ser obtido.</param>
        /// <returns>As informações do usuário com o ID especificado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {

                _logger.Trace("Não encontrou usuário {0}", id);
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="user">As informações do usuário a ser criado.</param>
        /// <returns>As informações do usuário recém-criado.</returns>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var createdUser = await _userService.CreateUserAsync(user);

            if (createdUser == null)
            {

                _logger.Trace("Usuário já criado com esse email {0}", user.Email);
                return Unauthorized("Usuário já criado com esse email.");
            }
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Atualiza um usuário pelo ID.
        /// </summary>
        /// <param name="id">O ID do usuário a ser atualizado.</param>
        /// <param name="user">As informações do usuário atualizado.</param>
        /// <returns>Nenhum conteúdo.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var updateUser = await _userService.UpdateUserAsync(user);
            if (updateUser == null)
            {

                _logger.Trace("Usuário não encontrado {0}", id);
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui um usuário pelo ID.
        /// </summary>
        /// <param name="id">O ID do usuário a ser excluído.</param>
        /// <returns>Nenhum conteúdo.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                _logger.Trace("Usuário não encontrado {0}", id);
                return NotFound();
            }
            return NoContent();
        }
    }
}
