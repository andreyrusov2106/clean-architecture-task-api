using MediatR;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Tasks;

// Команда реализует IRequest<TResponse>. Мы ожидаем вернуть DTO созданной задачи.
public record CreateTaskCommand(string Title, string Description) : IRequest<TaskItemDto>;