# Інструкції для налаштування Clerk

## 1. Автоматичне додавання ролі "User" для нових юзерів

Щоб автоматично додавати `public_metadata` з роллю "User" для всіх нових юзерів, потрібно налаштувати **Clerk Webhooks**.

### Крок 1: Створіть Webhook в Clerk Dashboard

1. Перейдіть в Clerk Dashboard: https://dashboard.clerk.com
2. Виберіть ваш проект
3. Перейдіть в **Webhooks** (в меню зліва)
4. Натисніть **Add Endpoint**
5. Введіть URL вашого API endpoint (наприклад: `https://your-api.com/webhooks/clerk`)
6. Виберіть події:
   - `user.created` - коли створюється новий юзер
   - `user.updated` - коли оновлюється юзер

### Крок 2: Створіть endpoint для обробки webhooks в API

Створіть новий контролер `WebhooksController.cs`:

```csharp
using Microsoft.AspNetCore.Mvc;
using Svix;
using System.Text.Json;

namespace API.Controllers;

[Route("webhooks")]
[ApiController]
public class WebhooksController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<WebhooksController> _logger;

    public WebhooksController(IConfiguration configuration, ILogger<WebhooksController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("clerk")]
    public async Task<IActionResult> HandleClerkWebhook()
    {
        var webhookSecret = _configuration["Clerk:WebhookSecret"];
        
        // Read the request body
        using var reader = new StreamReader(Request.Body);
        var payload = await reader.ReadToEndAsync();
        
        // Get Svix headers
        var headers = new WebHeaderCollection();
        headers.Add("svix-id", Request.Headers["svix-id"].ToString());
        headers.Add("svix-timestamp", Request.Headers["svix-timestamp"].ToString());
        headers.Add("svix-signature", Request.Headers["svix-signature"].ToString());

        try
        {
            var wh = new Webhook(webhookSecret);
            var evt = wh.Verify(payload, headers);
            
            var eventType = evt.GetProperty("type").GetString();
            
            if (eventType == "user.created")
            {
                var userData = evt.GetProperty("data");
                var userId = userData.GetProperty("id").GetString();
                
                // Update user metadata via Clerk API
                await UpdateUserMetadata(userId, new { role = "User" });
                
                _logger.LogInformation($"Added default role 'User' to new user: {userId}");
            }
            
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Webhook verification failed: {ex.Message}");
            return BadRequest();
        }
    }
    
    private async Task UpdateUserMetadata(string userId, object metadata)
    {
        var clerkApiKey = _configuration["Clerk:SecretKey"];
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {clerkApiKey}");
        
        var content = new StringContent(
            JsonSerializer.Serialize(new { public_metadata = metadata }),
            System.Text.Encoding.UTF8,
            "application/json"
        );
        
        await client.PatchAsync(
            $"https://api.clerk.com/v1/users/{userId}",
            content
        );
    }
}
```

### Крок 3: Додайте Clerk Secret Key в appsettings.json

```json
{
  "Clerk": {
    "Domain": "cool-snake-75.clerk.accounts.dev",
    "SecretKey": "sk_test_YOUR_SECRET_KEY",
    "WebhookSecret": "whsec_YOUR_WEBHOOK_SECRET"
  }
}
```

### Крок 4: Встановіть Svix NuGet package

```bash
dotnet add package Svix
```

## 2. Синхронізація оновлень юзера з Clerk

Для синхронізації оновлень профілю (ім'я, email, аватар) з Clerk, потрібно оновити методи в `UsersController.cs`.

### Додайте Clerk API клієнт

Створіть сервіс `ClerkApiService.cs`:

```csharp
using System.Text.Json;

namespace Infrastructure.Services;

public interface IClerkApiService
{
    Task UpdateUserAsync(string clerkId, object updates);
    Task UpdateUserMetadataAsync(string clerkId, object metadata);
}

public class ClerkApiService : IClerkApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ClerkApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        
        var apiKey = configuration["Clerk:SecretKey"];
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        _httpClient.BaseAddress = new Uri("https://api.clerk.com/v1/");
    }

    public async Task UpdateUserAsync(string clerkId, object updates)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(updates),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        await _httpClient.PatchAsync($"users/{clerkId}", content);
    }

    public async Task UpdateUserMetadataAsync(string clerkId, object metadata)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(new { public_metadata = metadata }),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        await _httpClient.PatchAsync($"users/{clerkId}", content);
    }
}
```

### Зареєструйте сервіс в DI

В `ConfigurePersistence.cs` або `Program.cs`:

```csharp
services.AddHttpClient<IClerkApiService, ClerkApiService>();
```

### Оновіть UpdateUserCommand

```csharp
public class UpdateUserCommandHandler(
    IUserRepository userRepository,
    IClerkApiService clerkApiService)
    : IRequestHandler<UpdateUserCommand, Result<User, Error>>
{
    public async Task<Result<User, Error>> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var userOption = await userRepository.GetById(new UserId(request.UserId), cancellationToken);
        
        if (!userOption.HasValue)
            return Result.Failure<User, Error>(Errors.User.NotFound);

        var user = userOption.ValueOrFailure();

        // Update local database
        user.UpdateProfile(request.Email, request.Name, request.Surname, request.PhoneNumber);
        await userRepository.Update(user, cancellationToken);

        // Sync with Clerk
        if (!string.IsNullOrEmpty(user.ClerkId))
        {
            await clerkApiService.UpdateUserAsync(user.ClerkId, new
            {
                first_name = request.Name,
                last_name = request.Surname,
                // Clerk doesn't support phone_number in basic update, 
                // you may need to use phone_numbers array
            });
        }

        return Result.Success<User, Error>(user);
    }
}
```

## 3. Альтернативний підхід (простіший)

Якщо не хочете налаштовувати webhooks, можете:

1. **Вручну додати роль в Clerk Dashboard** для кожного юзера:
   - Перейдіть в Users
   - Виберіть юзера
   - В розділі "Public metadata" додайте:
     ```json
     {
       "role": "User"
     }
     ```

2. **Або оновити ClerkUserSyncMiddleware**, щоб він автоматично оновлював Clerk metadata при створенні юзера (але це потребує Clerk API key на сервері).

## Примітки

- Clerk Webhooks працюють тільки для production та development середовищ з публічним URL
- Для локальної розробки використовуйте ngrok або подібні інструменти для тунелювання
- Webhook secret можна знайти в Clerk Dashboard після створення endpoint
