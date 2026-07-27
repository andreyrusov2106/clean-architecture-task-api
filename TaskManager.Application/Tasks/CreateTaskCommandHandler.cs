using MediatR;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Tasks;

// IRequestHandler<Команда, Результат>
public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskItemDto>
{
    private readonly ITaskRepository _repository;

    public CreateTaskCommandHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskItemDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        // 1. Создаем сущность домена (бизнес-логика инкапсулирована в конструкторе TaskItem)
        var task = new TaskItem(request.Title, request.Description);

        // 2. Сохраняем через интерфейс (не зная, как именно это делает Infrastructure)
        await _repository.AddAsync(task);

        // 3. Возвращаем DTO (вручную маппим, позже заменим на AutoMapper)
        return new TaskItemDto(
            task.Id,
            task.Title,
            task.Description,
            task.CreatedAt,
            task.IsCompleted
        );
    }
}