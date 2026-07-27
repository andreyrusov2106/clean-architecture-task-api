namespace TaskManager.Domain.Entities;

public class TaskItem
{
    // Свойство Id - это первичный ключ. 
    // В Domain слое мы не используем атрибуты [Key] из Entity Framework, 
    // чтобы Domain оставался чистым!
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public bool IsCompleted { get; private set; }

    // Конструктор по умолчанию нужен для EF Core
    private TaskItem() { }

    // Публичный конструктор для создания новой задачи
    public TaskItem(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        IsCompleted = false;
    }

    // Методы изменения состояния (Инкапсуляция!)
    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }

    public void UpdateDetails(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        Title = title;
        Description = description;
    }
}