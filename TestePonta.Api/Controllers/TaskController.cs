using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Security.Claims;
using TestePonta.Core.DTOs;
using TestePonta.Domain.Services;

/// <summary>
/// Controller para operações relacionadas a tarefas.
/// </summary>
[Route("api/tasks")]
[ApiController]
[Produces("application/json")]
[Authorize]
public class TaskController : ControllerBase
{

    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly ITaskService _taskService;

    /// <summary>
    /// Inicializa uma nova instância do controlador de tarefas.
    /// </summary>
    /// <param name="taskService">O serviço de tarefas a ser injetado.</param>
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

        _logger.Trace("Pedido de listagem de Tasks");
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    /// <summary>
    /// Obtém tarefas com base em seu status.
    /// </summary>
    /// <param name="id">O ID do status da tarefa.</param>
    [HttpGet("status/{id}")]
    public async Task<ActionResult<TaskDTO>> GetTaskByStatusId(int id)
    {
        var task = await _taskService.GetTasksByStatusAsync((TestePonta.Core.Enums.TaskStatus)id);
        if (task == null)
        {

            _logger.Trace("Task não encontrada para Status: {0}", (TestePonta.Core.Enums.TaskStatus)id);
            return NotFound();
        }
        return Ok(task);
    }

    /// <summary>
    /// Obtém uma tarefa pelo ID.
    /// </summary>
    /// <param name="id">O ID da tarefa.</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDTO>> GetTaskById(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            _logger.Trace("Task não encontrada: {0}", id);
            return NotFound();
        }
        return Ok(task);
    }

    /// <summary>
    /// Cria uma nova tarefa.
    /// </summary>
    /// <param name="task">Os detalhes da tarefa a serem criados.</param>
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
    /// <param name="id">O ID da tarefa a ser atualizada.</param>
    /// <param name="taskDto">Os detalhes da tarefa atualizada.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, TaskDTO taskDto)
    {
        if (id != taskDto.Id)
        {

            _logger.Trace("Task inválida: {0}", id);
            return BadRequest();
        }

        if (!await IsOwner(id))
        {

            _logger.Trace("Usuário proibido de alterar Task {0}", id);
            return Unauthorized("Você precisa ter criado a tarefa para alterá-la.");
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
    /// <param name="id">O ID da tarefa a ser excluída.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        if (!await IsOwner(id))
        {
            return Unauthorized("Você precisa ter criado a tarefa para deletá-la.");
        }

        var result = await _taskService.DeleteTaskAsync(id);
        if (!result)
        {

            _logger.Trace("Task não encontrada: {0}", id);
            return NotFound();
        }
        return NoContent();
    }

    /// <summary>
    /// Verifica se o usuário logado é o proprietário da tarefa.
    /// </summary>
    /// <param name="taskId">O ID da tarefa a ser verificada.</param>
    /// <returns>Verdadeiro se o usuário for o proprietário, falso caso contrário.</returns>
    private async Task<bool> IsOwner(int taskId)
    {
        var task = await _taskService.GetTaskByIdAsync(taskId);
        if (task == null)
        {
            
            _logger.Trace("Task não encontrada: {0}", taskId);
            return false;
        }
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userId != null && task.UserId.ToString() != userId)
        {

            _logger.Trace("Usuário {0} não tem permissão para task {1}", userId, taskId);
            return false;
        }
        return true;
    }
}
