using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tasks;

// Запрос тоже IRequest, но возвращает коллекцию.
public record GetAllTasksQuery : IRequest<IEnumerable<TaskItemDto>>;