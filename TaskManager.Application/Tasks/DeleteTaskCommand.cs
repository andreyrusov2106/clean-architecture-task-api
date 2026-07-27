using MediatR;

namespace TaskManager.Application.Tasks;

// Команда ничего не возвращает (Unit = void в мире MediatR)
public record DeleteTaskCommand(Guid Id) : IRequest<Unit>;