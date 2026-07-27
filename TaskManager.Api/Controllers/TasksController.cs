using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.Application.DTOs;
using TaskManager.Application.Tasks;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    // Mediator инжектится через DI
    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/tasks
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetAll()
    {
        var query = new GetAllTasksQuery();
        var tasks = await _mediator.Send(query);
        return Ok(tasks);
    }

    // POST: api/tasks
    [HttpPost]
    public async Task<ActionResult<TaskItemDto>> Create([FromBody] CreateTaskItemDto dto)
    {
        var command = new CreateTaskCommand(dto.Title, dto.Description);
        var createdTask = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = createdTask.Id }, createdTask);
    }

    // 1. В контроллере (TaskManager.Api/Controllers/TasksController.cs)
    [HttpDelete("{id}")] // ID берется из URL: DELETE /api/tasks/123e4567-e89b-12d3...
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteTaskCommand(id);
        await _mediator.Send(command);

        // 204 No Content - стандарт REST для успешного удаления
        return NoContent();
    }
}