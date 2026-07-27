using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Tasks;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskItemDto>>
{
    private readonly ITaskRepository _repository;

    public GetAllTasksQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskItemDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _repository.GetAllAsync();

        // Маппинг коллекции Entity в коллекцию DTO
        return tasks.Select(t => new TaskItemDto(
            t.Id,
            t.Title,
            t.Description,
            t.CreatedAt,
            t.IsCompleted
        ));
    }
}