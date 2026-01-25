using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Interfaces.Queries;
using Application.Services.GeminiService;
using Domain.Problems;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class GeminiService : IGeminiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IProblemQueries _problemQueries;
    private readonly IRatingQueries _ratingQueries;
    private readonly ILogger<GeminiService> _logger;
    
    private const string GeminiFlashModel = "gemini-2.5-flash";
    private const string GeminiProModel = "gemini-2.5-pro";

    public GeminiService(
        HttpClient httpClient,
        IConfiguration configuration,
        IProblemQueries problemQueries,
        IRatingQueries ratingQueries,
        ILogger<GeminiService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _problemQueries = problemQueries;
        _ratingQueries = ratingQueries;
        _logger = logger;
    }

    public async Task<ChatResponse> ProcessChatMessageAsync(ChatRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            var intent = await ClassifyIntentAsync(request.Message, cancellationToken);
            
            return intent switch
            {
                ChatIntent.GetTopProblems => await HandleGetTopProblemsAsync(request, cancellationToken),
                ChatIntent.GetProblemsByCategory => await HandleGetProblemsByCategoryAsync(request, cancellationToken),
                ChatIntent.GetProblemsByStatus => await HandleGetProblemsByStatusAsync(request, cancellationToken),
                ChatIntent.GetProblemsByPriority => await HandleGetProblemsByPriorityAsync(request, cancellationToken),
                ChatIntent.GetRecentProblems => await HandleGetRecentProblemsAsync(request, cancellationToken),
                ChatIntent.GetMyProblems => await HandleGetMyProblemsAsync(request, cancellationToken),
                ChatIntent.HowToUse => await HandleHowToUseAsync(request, cancellationToken),
                ChatIntent.WhatIsThisSite => await HandleWhatIsThisSiteAsync(request, cancellationToken),
                ChatIntent.ContactAuthors => await HandleContactAuthorsAsync(request, cancellationToken),
                ChatIntent.RoleCapabilities => await HandleRoleCapabilitiesAsync(request, cancellationToken),
                ChatIntent.GeneralQuestion => await HandleGeneralQuestionAsync(request, cancellationToken),
                _ => await HandleGeneralQuestionAsync(request, cancellationToken)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing chat message");
            return new ChatResponse(
                "Вибачте, сталася помилка при обробці вашого запиту. Спробуйте ще раз.",
                ChatResponseType.Error
            );
        }
    }

    private async Task<ChatIntent> ClassifyIntentAsync(string message, CancellationToken cancellationToken)
    {
        var prompt = $"""
                      Класифікуй намір користувача на основі його повідомлення. Відповідай ТІЛЬКИ одним словом з наступного списку:
                      - TOP_PROBLEMS (якщо питає про найкращі/найгірші/топ проблеми за рейтингом)
                      - CATEGORY_PROBLEMS (якщо питає про проблеми певної категорії: дороги, освітлення, сміття, водопостачання, транспорт, парки, безпека, шум, тварини)
                      - STATUS_PROBLEMS (якщо питає про проблеми за статусом: нові, в роботі, виконані, відхилені)
                      - PRIORITY_PROBLEMS (якщо питає про проблеми за пріоритетом: низький, середній, високий, критичний)
                      - RECENT_PROBLEMS (якщо питає про останні/нещодавні проблеми)
                      - MY_PROBLEMS (якщо питає про свої проблеми)
                      - HOW_TO_USE (якщо питає як користуватися сайтом/системою)
                      - WHAT_IS_SITE (якщо питає що це за сайт/для чого він)
                      - CONTACT_AUTHORS (якщо хоче зв'язатися з авторами/підтримкою)
                      - ROLE_CAPABILITIES (якщо питає що може робити його роль/які права)
                      - GENERAL (для всіх інших питань)

                      Повідомлення користувача: "{message}"

                      Відповідь:
                      """;

        var response = await CallGeminiAsync(prompt, GeminiFlashModel, cancellationToken);
        
        return response.Trim().ToUpperInvariant() switch
        {
            "TOP_PROBLEMS" => ChatIntent.GetTopProblems,
            "CATEGORY_PROBLEMS" => ChatIntent.GetProblemsByCategory,
            "STATUS_PROBLEMS" => ChatIntent.GetProblemsByStatus,
            "PRIORITY_PROBLEMS" => ChatIntent.GetProblemsByPriority,
            "RECENT_PROBLEMS" => ChatIntent.GetRecentProblems,
            "MY_PROBLEMS" => ChatIntent.GetMyProblems,
            "HOW_TO_USE" => ChatIntent.HowToUse,
            "WHAT_IS_SITE" => ChatIntent.WhatIsThisSite,
            "CONTACT_AUTHORS" => ChatIntent.ContactAuthors,
            "ROLE_CAPABILITIES" => ChatIntent.RoleCapabilities,
            _ => ChatIntent.GeneralQuestion
        };
    }

    private async Task<ChatResponse> HandleGetTopProblemsAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var allProblems = await _problemQueries.GetAll(cancellationToken);
        var problemsWithRatings = new List<(Problem Problem, double AvgRating)>();

        foreach (var problem in allProblems)
        {
            var ratings = await _ratingQueries.GetByProblemId(problem.Id, cancellationToken);
            var avgRating = ratings.Any() ? ratings.Average(r => r.Points) : 0;
            problemsWithRatings.Add((problem, avgRating));
        }

        var topProblems = problemsWithRatings
            .OrderByDescending(p => p.AvgRating)
            .Take(5)
            .Select(p => MapToProblemSummary(p.Problem, p.AvgRating))
            .ToList();

        var message = topProblems.Any()
            ? "Ось топ-5 проблем з найвищим рейтингом:"
            : "На жаль, поки немає проблем з рейтингами.";

        return new ChatResponse(message, ChatResponseType.ProblemsList, topProblems);
    }

    private async Task<ChatResponse> HandleGetProblemsByCategoryAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var category = ExtractCategory(request.Message);
        var allProblems = await _problemQueries.GetAll(cancellationToken);
        
        var filteredProblems = allProblems
            .Where(p => p.Categories.Any(c => c.Value.Contains(category, StringComparison.OrdinalIgnoreCase)))
            .Take(5)
            .ToList();

        var problemSummaries = new List<ProblemSummaryForChat>();
        foreach (var problem in filteredProblems)
        {
            var ratings = await _ratingQueries.GetByProblemId(problem.Id, cancellationToken);
            var avgRating = ratings.Any() ? ratings.Average(r => r.Points) : 0;
            problemSummaries.Add(MapToProblemSummary(problem, avgRating));
        }

        var message = problemSummaries.Any()
            ? $"Ось проблеми в категорії '{category}' (топ-5):"
            : $"На жаль, не знайдено проблем у категорії '{category}'.";

        return new ChatResponse(message, ChatResponseType.ProblemsList, problemSummaries);
    }

    private async Task<ChatResponse> HandleGetProblemsByStatusAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var statusText = ExtractStatus(request.Message);
        ProblemStatus? status = null;
        
        try
        {
            status = ProblemStatus.From(statusText);
        }
        catch
        {
            return new ChatResponse(
                $"Не вдалося визначити статус. Доступні статуси: Нова, В роботі, Виконано, Відхилено.",
                ChatResponseType.Text
            );
        }

        var problems = await _problemQueries.GetByStatus(status, cancellationToken);
        var topProblems = problems.Take(5).ToList();

        var problemSummaries = new List<ProblemSummaryForChat>();
        foreach (var problem in topProblems)
        {
            var ratings = await _ratingQueries.GetByProblemId(problem.Id, cancellationToken);
            var avgRating = ratings.Any() ? ratings.Average(r => r.Points) : 0;
            problemSummaries.Add(MapToProblemSummary(problem, avgRating));
        }

        var message = problemSummaries.Any()
            ? $"Ось проблеми зі статусом '{statusText}' (топ-5):"
            : $"На жаль, не знайдено проблем зі статусом '{statusText}'.";

        return new ChatResponse(message, ChatResponseType.ProblemsList, problemSummaries);
    }

    private async Task<ChatResponse> HandleGetProblemsByPriorityAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var priorityText = ExtractPriority(request.Message);
        var allProblems = await _problemQueries.GetAll(cancellationToken);
        
        var filteredProblems = allProblems
            .Where(p => p.Priority.Value.Contains(priorityText, StringComparison.OrdinalIgnoreCase))
            .Take(5)
            .ToList();

        var problemSummaries = new List<ProblemSummaryForChat>();
        foreach (var problem in filteredProblems)
        {
            var ratings = await _ratingQueries.GetByProblemId(problem.Id, cancellationToken);
            var avgRating = ratings.Any() ? ratings.Average(r => r.Points) : 0;
            problemSummaries.Add(MapToProblemSummary(problem, avgRating));
        }

        var message = problemSummaries.Any()
            ? $"Ось проблеми з пріоритетом '{priorityText}' (топ-5):"
            : $"На жаль, не знайдено проблем з пріоритетом '{priorityText}'.";

        return new ChatResponse(message, ChatResponseType.ProblemsList, problemSummaries);
    }

    private async Task<ChatResponse> HandleGetRecentProblemsAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var allProblems = await _problemQueries.GetAll(cancellationToken);
        var recentProblems = allProblems
            .OrderByDescending(p => p.CreatedAt)
            .Take(5)
            .ToList();

        var problemSummaries = new List<ProblemSummaryForChat>();
        foreach (var problem in recentProblems)
        {
            var ratings = await _ratingQueries.GetByProblemId(problem.Id, cancellationToken);
            var avgRating = ratings.Any() ? ratings.Average(r => r.Points) : 0;
            problemSummaries.Add(MapToProblemSummary(problem, avgRating));
        }

        var message = problemSummaries.Any()
            ? "Ось 5 найновіших проблем:"
            : "На жаль, поки немає зареєстрованих проблем.";

        return new ChatResponse(message, ChatResponseType.ProblemsList, problemSummaries);
    }

    private async Task<ChatResponse> HandleGetMyProblemsAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        if (!request.UserId.HasValue)
        {
            return new ChatResponse(
                "Щоб переглянути свої проблеми, вам потрібно увійти в систему.",
                ChatResponseType.Text
            );
        }

        var userProblems = await _problemQueries.GetByUserId(
            new Domain.Identity.Users.UserId(request.UserId.Value), 
            cancellationToken);
        
        var topProblems = userProblems.Take(5).ToList();

        var problemSummaries = new List<ProblemSummaryForChat>();
        foreach (var problem in topProblems)
        {
            var ratings = await _ratingQueries.GetByProblemId(problem.Id, cancellationToken);
            var avgRating = ratings.Any() ? ratings.Average(r => r.Points) : 0;
            problemSummaries.Add(MapToProblemSummary(problem, avgRating));
        }

        var message = problemSummaries.Any()
            ? $"Ось ваші проблеми (показано {problemSummaries.Count} з {userProblems.Count}):"
            : "Ви ще не створювали жодної проблеми.";

        return new ChatResponse(message, ChatResponseType.ProblemsList, problemSummaries);
    }

    private Task<ChatResponse> HandleHowToUseAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var roleSpecificInstructions = request.UserRole switch
        {
            "Administrator" => """

                               **Як адміністратор, ви можете:**
                               - Переглядати всі проблеми на карті та в списку
                               - Керувати користувачами (створювати, редагувати, видаляти)
                               - Призначати координаторів до проблем
                               - Змінювати статуси та пріоритети проблем
                               - Видаляти будь-які коментарі
                               - Переглядати статистику системи

                               **Швидкий старт:**
                               1. Перейдіть на сторінку 'Користувачі' для керування акаунтами
                               2. На карті ви бачите всі активні проблеми
                               3. Клікніть на проблему для детального перегляду та керування
                               """,

            "Coordinator" => """

                             **Як координатор, ви можете:**
                             - Переглядати призначені вам проблеми
                             - Змінювати статус проблем (В роботі, Виконано, Відхилено)
                             - Додавати коментарі координатора
                             - Завантажувати фото прогресу виконання
                             - Оновлювати поточний стан робіт

                             **Швидкий старт:**
                             1. Перейдіть на сторінку 'Мої завдання'
                             2. Оберіть проблему для роботи
                             3. Оновлюйте статус та додавайте коментарі про прогрес
                             """,

            _ => """

                 **Як користувач, ви можете:**
                 - Переглядати карту з проблемами міста
                 - Створювати нові звіти про проблеми
                 - Додавати фотографії до своїх звітів
                 - Коментувати проблеми
                 - Оцінювати якість вирішення проблем
                 - Відстежувати статус своїх звітів

                 **Швидкий старт:**
                 1. Натисніть 'Повідомити про проблему'
                 2. Вкажіть місце на карті
                 3. Опишіть проблему та додайте фото
                 4. Відстежуйте статус у розділі 'Мої звіти'
                 """
        };

        var message = $"""
                       # Як користуватися Ostroh Problems

                                   {roleSpecificInstructions}

                                   **Потрібна допомога?** Запитайте мене про будь-що!
                       """;

        return Task.FromResult(new ChatResponse(message, ChatResponseType.Help));
    }

    private Task<ChatResponse> HandleWhatIsThisSiteAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var message = """
                      # Ostroh Problems - Платформа для вирішення міських проблем

                      **Ostroh Problems** — це веб-платформа для звітування, відстеження та вирішення міських проблем в місті Острог.

                      ## Основні можливості:
                      - 📍 **Інтерактивна карта** — бачте всі проблеми міста на карті
                      - 📝 **Звітування** — повідомляйте про проблеми з фото та геолокацією
                      - 🔄 **Відстеження** — слідкуйте за статусом вирішення
                      - 💬 **Коментарі** — обговорюйте проблеми з іншими
                      - ⭐ **Оцінки** — оцінюйте якість вирішення

                      ## Категорії проблем:
                      Дороги, Освітлення, Сміття, Водопостачання, Громадський транспорт, Парки та зелені зони, Безпека, Шум, Тварини та інше.

                      ## Ролі в системі:
                      - **Користувач** — створює звіти та відстежує їх
                      - **Координатор** — працює над вирішенням проблем
                      - **Адміністратор** — керує системою та користувачами

                      Це демо-проект для демонстрації можливостей системи.
                      """;

        return Task.FromResult(new ChatResponse(message, ChatResponseType.Help));
    }

    private Task<ChatResponse> HandleContactAuthorsAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var message = """
                      # Зв'язок з авторами

                      **Ostroh Problems** — це демо-проект, створений для демонстрації можливостей сучасних веб-технологій.

                      ## Контакти:
                      - 📧 **Email:** support@ostrohproblems.demo
                      - 🌐 **GitHub:** github.com/ostroh-problems
                      - 💼 **LinkedIn:** Профіль розробника

                      ## Зворотній зв'язок:
                      Якщо у вас є пропозиції щодо покращення системи або ви знайшли помилку, будь ласка, створіть issue на GitHub.

                      ## Технічна підтримка:
                      Для технічних питань пишіть на email з темою 'Technical Support'.

                      *Примітка: Це демо-версія, тому контакти є ілюстративними.*
                      """;

        return Task.FromResult(new ChatResponse(message, ChatResponseType.Help));
    }

    private Task<ChatResponse> HandleRoleCapabilitiesAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var message = request.UserRole switch
        {
            "Administrator" => """
                               # Можливості адміністратора

                               Ви маєте **повний доступ** до системи:

                               ## Керування користувачами:
                               - ✅ Створення нових користувачів
                               - ✅ Редагування профілів
                               - ✅ Призначення ролей
                               - ✅ Видалення акаунтів

                               ## Керування проблемами:
                               - ✅ Перегляд всіх проблем
                               - ✅ Призначення координаторів
                               - ✅ Зміна статусів та пріоритетів
                               - ✅ Видалення проблем

                               ## Модерація:
                               - ✅ Видалення коментарів
                               - ✅ Перегляд всієї активності
                               """,

            "Coordinator" => """
                             # Можливості координатора

                             Ви відповідаєте за **вирішення проблем**:

                             ## Робота з проблемами:
                             - ✅ Перегляд призначених проблем
                             - ✅ Зміна статусу (В роботі → Виконано/Відхилено)
                             - ✅ Додавання коментарів координатора
                             - ✅ Завантаження фото прогресу
                             - ✅ Оновлення поточного стану

                             ## Обмеження:
                             - ❌ Не можете видаляти чужі проблеми
                             - ❌ Не можете керувати користувачами
                             - ❌ Не можете призначати інших координаторів
                             """,

            _ => """
                 # Можливості користувача

                 Ви можете **звітувати про проблеми**:

                 ## Створення звітів:
                 - ✅ Створення нових проблем
                 - ✅ Додавання фотографій
                 - ✅ Вибір категорії та локації
                 - ✅ Редагування своїх звітів

                 ## Взаємодія:
                 - ✅ Коментування проблем
                 - ✅ Оцінювання вирішених проблем
                 - ✅ Відстеження статусу своїх звітів

                 ## Обмеження:
                 - ❌ Не можете змінювати статус проблем
                 - ❌ Не можете видаляти чужі звіти
                 - ❌ Не можете призначати координаторів
                 """
        };

        return Task.FromResult(new ChatResponse(message, ChatResponseType.Help));
    }

    private async Task<ChatResponse> HandleGeneralQuestionAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var systemPrompt = GetSystemPrompt(request.UserRole);
        var prompt = $"""
                      {systemPrompt}

                                  Питання користувача: {request.Message}

                                  Дай корисну та дружню відповідь українською мовою. Якщо питання стосується проблем міста, запропонуй конкретні дії.
                      """;

        var response = await CallGeminiAsync(prompt, GeminiFlashModel, cancellationToken);
        return new ChatResponse(response, ChatResponseType.Text);
    }

    private async Task<string> CallGeminiAsync(string prompt, string model, CancellationToken cancellationToken)
    {
        var apiKey = _configuration["Gemini:ApiKey"];
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("Gemini API key is not configured");
        }

        var url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            },
            generationConfig = new
            {
                temperature = 0.7,
                maxOutputTokens = 1024
            }
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(url, content, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Gemini API error: {StatusCode} - {Content}", response.StatusCode, responseContent);
            throw new Exception($"Gemini API error: {response.StatusCode}");
        }

        var jsonResponse = JsonSerializer.Deserialize<GeminiResponse>(responseContent);
        return jsonResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text ?? "";
    }

    private string GetSystemPrompt(string userRole)
    {
        return $"""
                Ти - AI асистент платформи Ostroh Problems для вирішення міських проблем в місті Острог.

                            Про платформу:
                            - Це веб-додаток для звітування та відстеження міських проблем
                            - Користувачі можуть створювати звіти з фото та геолокацією
                            - Координатори працюють над вирішенням проблем
                            - Адміністратори керують системою

                            Категорії проблем: Дороги, Освітлення, Сміття, Водопостачання, Громадський транспорт, Парки та зелені зони, Безпека, Шум, Тварини, Інше.

                            Статуси: Нова, В роботі, Виконано, Відхилено.
                            Пріоритети: Низький, Середній, Високий, Критичний.

                            Поточна роль користувача: {userRole}

                            Правила:
                            1. Відповідай українською мовою
                            2. Будь дружнім та корисним
                            3. Якщо не знаєш відповіді - скажи про це чесно
                            4. Пропонуй конкретні дії для вирішення питань
                            5. Це демо-проект, тому деякі дані є ілюстративними
                """;
    }

    private ProblemSummaryForChat MapToProblemSummary(Problem problem, double avgRating)
    {
        return new ProblemSummaryForChat(
            problem.Id.Value,
            problem.Title,
            problem.Description.Length > 100 ? problem.Description[..100] + "..." : problem.Description,
            problem.Status.Value,
            problem.Priority.Value,
            problem.Latitude,
            problem.Longitude,
            problem.Categories.Select(c => c.Value).ToList(),
            avgRating > 0 ? avgRating : null,
            problem.Comments.Count,
            problem.CreatedAt,
            problem.CreatedBy?.Name ?? "Анонім"
        );
    }

    private string ExtractCategory(string message)
    {
        var categories = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "дорог", "Дороги" },
            { "освітл", "Освітлення" },
            { "сміт", "Сміття" },
            { "вод", "Водопостачання" },
            { "транспорт", "Громадський транспорт" },
            { "парк", "Парки та зелені зони" },
            { "безпек", "Безпека" },
            { "шум", "Шум" },
            { "тварин", "Тварини" }
        };

        foreach (var kvp in categories)
        {
            if (message.Contains(kvp.Key, StringComparison.OrdinalIgnoreCase))
                return kvp.Value;
        }

        return "Інше";
    }

    private string ExtractStatus(string message)
    {
        if (message.Contains("нов", StringComparison.OrdinalIgnoreCase))
            return "Нова";
        if (message.Contains("робот", StringComparison.OrdinalIgnoreCase) || message.Contains("прогрес", StringComparison.OrdinalIgnoreCase))
            return "В роботі";
        if (message.Contains("викон", StringComparison.OrdinalIgnoreCase) || message.Contains("завершен", StringComparison.OrdinalIgnoreCase))
            return "Виконано";
        if (message.Contains("відхил", StringComparison.OrdinalIgnoreCase))
            return "Відхилено";

        return "Нова";
    }

    private string ExtractPriority(string message)
    {
        if (message.Contains("критич", StringComparison.OrdinalIgnoreCase))
            return "Критичний";
        if (message.Contains("висок", StringComparison.OrdinalIgnoreCase))
            return "Високий";
        if (message.Contains("середн", StringComparison.OrdinalIgnoreCase))
            return "Середній";
        if (message.Contains("низьк", StringComparison.OrdinalIgnoreCase))
            return "Низький";

        return "Середній";
    }
}

internal enum ChatIntent
{
    GetTopProblems,
    GetProblemsByCategory,
    GetProblemsByStatus,
    GetProblemsByPriority,
    GetRecentProblems,
    GetMyProblems,
    HowToUse,
    WhatIsThisSite,
    ContactAuthors,
    RoleCapabilities,
    GeneralQuestion
}

internal class GeminiResponse
{
    [JsonPropertyName("candidates")]
    public List<GeminiCandidate>? Candidates { get; set; }
}

internal class GeminiCandidate
{
    [JsonPropertyName("content")]
    public GeminiContent? Content { get; set; }
}

internal class GeminiContent
{
    [JsonPropertyName("parts")]
    public List<GeminiPart>? Parts { get; set; }
}

internal class GeminiPart
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}
