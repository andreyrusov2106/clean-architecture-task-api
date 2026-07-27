using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // В реальном проекте это берется из appsettings.json!
    private const string JwtKey = "MySuperSecretKeyForJWTAuthentication123!";
    private const string JwtIssuer = "TaskManagerApi";
    private const string JwtAudience = "TaskManagerClients";

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // 1. Проверяем логин/пароль (здесь заглушка, в реальности запрос к БД)
        if (request.Username != "admin" || request.Password != "123")
        {
            return Unauthorized("Неверный логин или пароль");
        }

        // 2. Создаем "полезную нагрузку" (Claims)
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "User") // Можно добавить роль
        };

        // 3. Создаем ключ для подписи
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // 4. Генерируем токен
        var token = new JwtSecurityToken(
            issuer: JwtIssuer,
            audience: JwtAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // Токен живет 1 час
            signingCredentials: creds
        );

        // 5. Возвращаем токен клиенту
        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}

// Простой класс для приема данных
public record LoginRequest(string Username, string Password);