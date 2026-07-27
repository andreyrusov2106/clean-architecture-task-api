using MediatR;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Tasks;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Unit>
{
    private readonly ITaskRepository _repository;

    public DeleteTaskCommandHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        // Можно добавить проверку: если задачи нет, выбросить NotFoundException
        await _repository.DeleteAsync(request.Id);
        return Unit.Value; // Возвращаем пустой результат
    }
}