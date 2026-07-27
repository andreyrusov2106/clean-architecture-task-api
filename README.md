# 🧱 Clean Architecture Task API

> REST API для управления задачами, построенный по принципам Clean Architecture и CQRS.

[![.NET 8](https://img.shields.io/badge/.NET-8.0-5126C1?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-336791?logo=postgresql&logoColor=white)](https://www.postgresql.org/)

---

## 🌟 Ключевые особенности
- **Clean Architecture:** Четкое разделение на 4 слоя (Domain, Application, Infrastructure, API) для независимости бизнес-логики.
- **CQRS:** Использование MediatR для разделения команд (Commands) и запросов (Queries).
- **Безопасность:** JWT аутентификация и авторизация эндпоинтов.
- **Логирование:** Структурированное логирование HTTP-запросов через Serilog.

---

## 🚀 Как запустить
1. Убедитесь, что у вас установлена .NET 8 SDK и запущен PostgreSQL.
2. Обновите строку подключения в `TaskManager.Api/Program.cs` (или через переменные окружения).
3. Запустите проект:
```bash
cd TaskManager.Api
dotnet run
4. Откройте Swagger UI по адресу: http://localhost:5000/swagger (порт может отличаться).
