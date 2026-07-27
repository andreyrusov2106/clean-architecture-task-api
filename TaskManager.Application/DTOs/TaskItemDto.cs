namespace TaskManager.Application.DTOs;

// Используем record — это современный C# (начиная с 9.0) для неизменяемых DTO.
// Он автоматически создает конструктор, свойства и методы Equals/GetHashCode.
public record TaskItemDto(
    Guid Id,
    string Title,
    string Description,
    DateTime CreatedAt,
    bool IsCompleted
);

// DTO для создания задачи (без Id и CreatedAt, их создаст система)
public record CreateTaskItemDto(string Title, string Description);

// DTO для обновления задачи
public record UpdateTaskItemDto(string Title, string Description);