using Microsoft.AspNetCore.Mvc;
using TestePonta.Core.DTOs;
using TestePonta.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace TestePonta.Controllers
{
    /// <summary>
    /// TaskController.
    /// </summary>
    [Route("api/users")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Construtor da TaskController.
        /// </summary>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtém todas as tarefas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Obtém uma tarefa pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Cria uma nova tarefa.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateTask(UserDTO user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Atualiza uma tarefa pelo ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UserDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var updateUser = await _userService.UpdateUserAsync(user);
            if (updateUser == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui uma tarefa pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
