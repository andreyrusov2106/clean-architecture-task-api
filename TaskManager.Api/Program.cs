using Serilog;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repositories;

// 1. Настройка Serilog ДО создания WebApplication
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File("logs/taskmanager-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Запуск приложения TaskManager API...");

    var builder = WebApplication.CreateBuilder(args);

    // 2. Подключаем Serilog к хосту .NET
    builder.Host.UseSerilog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var connectionString = "Host=localhost;Port=5432;Database=taskmanager_db;Username=postgres;Password=postgres";
    builder.Services.AddDbContext<TaskManagerDbContext>(options =>
        options.UseNpgsql(connectionString));

    builder.Services.AddScoped<ITaskRepository, TaskRepository>();
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TaskManager.Application.Tasks.CreateTaskCommand).Assembly));

    // Настройка JWT
    var jwtKey = "MySuperSecretKeyForJWTAuthentication123!"; // В реальном проекте это берется из appsettings.json!
    var jwtIssuer = "TaskManagerApi";
    var jwtAudience = "TaskManagerClients";

    builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtIssuer,
                ValidateAudience = true,
                ValidAudience = jwtAudience,
                ValidateLifetime = true,
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(jwtKey))
            };
        });

    builder.Services.AddAuthorization(); // Включаем авторизацию

    var app = builder.Build();

    // ... middleware ...

    app.UseAuthentication(); // ВАЖНО: Сначала аутентификация
    app.UseAuthorization();  // Потом авторизация

    app.MapControllers();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // 3. Добавляем middleware для логирования HTTP-запросов
    app.UseSerilogRequestLogging();

    app.MapControllers();

    Log.Information("Приложение успешно запущено");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Приложение завершилось с критической ошибкой");
}
finally
{
    Log.CloseAndFlush();
}