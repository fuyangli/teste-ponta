using Microsoft.AspNetCore.Mvc;
using TestePonta.Core.DTOs;
using TestePonta.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TestePonta.Controllers
{
    /// <summary>
    /// TaskController.
    /// </summary>
    [Route("api/tasks")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        /// <summary>
        /// Construtor da TaskController.
        /// </summary>
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Obtém todas as tarefas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// Obtém uma tarefa pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDTO>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        /// <summary>
        /// Cria uma nova tarefa.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TaskDTO>> CreateTask(TaskDTO task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            task.UserId = int.Parse(userId);
            var createdTask = await _taskService.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        /// <summary>
        /// Atualiza uma tarefa pelo ID.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskDTO taskDto)
        {
            if (id != taskDto.Id)
            {
                return BadRequest();
            }

            if (!await IsOwner(id))
            {
                return Unauthorized("Você precisa ter criado a tarefa para altera-la.");
            }

            var updatedTask = await _taskService.UpdateTaskAsync(taskDto);
            if (updatedTask == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Exclui uma tarefa pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if(!await IsOwner(id))
            {
                return Unauthorized("Você precisa ter criado a tarefa para deleta-la.");
            }

            var result = await _taskService.DeleteTaskAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        private async Task<bool> IsOwner(int taskId)
        {
            var task = await _taskService.GetTaskByIdAsync(taskId);
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId != null && task.UserId.ToString() != userId)
            {
                return false;
            }
            return true;
        }
    }
}
