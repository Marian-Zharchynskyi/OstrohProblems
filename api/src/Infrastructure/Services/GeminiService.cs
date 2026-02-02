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
    private const string GeminiLiteModel = "gemini-2.5-flash";
    private const string GeminiProModel = "gemini-2.5-pro";
    private const string GeminiEmbeddingModel = "text-embedding-004";

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
                ChatIntent.AnalyzeProblems => await HandleAnalyzeProblemsAsync(request, cancellationToken),
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

    public async Task<string> TranscribeAudioAsync(Stream audioStream, CancellationToken cancellationToken = default)
    {
        try
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("Gemini API key is not configured");
            }

            // Read audio stream to byte array
            using var memoryStream = new MemoryStream();
            await audioStream.CopyToAsync(memoryStream, cancellationToken);
            var audioBytes = memoryStream.ToArray();
            var base64Audio = Convert.ToBase64String(audioBytes);

            // Use Gemini's native multimodal capabilities to process audio
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{GeminiFlashModel}:generateContent?key={apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new object[]
                        {
                            new
                            {
                                text = "Прослухай це голосове повідомлення і транскрибуй його в текст. Відповідай ТІЛЬКИ текстом повідомлення, без додаткових коментарів. Якщо це питання про міські проблеми, збережи всі деталі (адреса, тип проблеми, опис)."
                            },
                            new
                            {
                                inline_data = new
                                {
                                    mime_type = "audio/webm",
                                    data = base64Audio
                                }
                            }
                        }
                    }
                },
                generationConfig = new
                {
                    temperature = 0.3,
                    maxOutputTokens = 512
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
                _logger.LogError("Gemini audio processing error: {StatusCode} - {Content}", response.StatusCode, responseContent);
                return "Не вдалося розпізнати голос. Спробуйте ще раз.";
            }

            var jsonResponse = JsonSerializer.Deserialize<GeminiResponse>(responseContent);
            var transcript = jsonResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

            return transcript?.Trim() ?? "Не вдалося розпізнати голос. Спробуйте говорити чіткіше.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error transcribing audio with Gemini");
            return "Помилка при розпізнаванні голосу.";
        }
    }

    public async Task<ExtractedProblemData> ExtractProblemDataAsync(string userMessage, CancellationToken cancellationToken = default)
    {
        try
        {
            var prompt = GetProblemExtractionPrompt(userMessage);
            var response = await CallGeminiAsyncExtended(prompt, GeminiFlashModel, cancellationToken, maxTokens: 2048, temperature: 0.3);
            return ParseExtractedProblemData(response, userMessage);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error extracting problem data");
            return new ExtractedProblemData(
                Title: "",
                Description: userMessage,
                Categories: new List<string>(),
                Priority: "Середній",
                StreetName: null,
                Latitude: null,
                Longitude: null,
                AiMessage: "Не вдалося обробити запит. Спробуйте описати проблему детальніше.",
                IsComplete: false
            );
        }
    }

    public async Task<ExtractedProblemData> ExtractProblemDataFromVoiceAsync(Stream audioStream, CancellationToken cancellationToken = default)
    {
        try
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("Gemini API key is not configured");
            }

            using var memoryStream = new MemoryStream();
            await audioStream.CopyToAsync(memoryStream, cancellationToken);
            var audioBytes = memoryStream.ToArray();
            var base64Audio = Convert.ToBase64String(audioBytes);

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{GeminiFlashModel}:generateContent?key={apiKey}";

            var extractionPrompt = @"Прослухай це голосове повідомлення українською мовою. Користувач описує міську проблему для звіту в місті Острог, Рівненська область, Україна.
Витягни з повідомлення наступну інформацію та поверни у форматі JSON:
{
  ""title"": ""короткий заголовок проблеми (до 100 символів)"",
  ""description"": ""детальний опис проблеми"",
  ""categories"": [""категорія1""],
  ""priority"": ""пріоритет"",
  ""streetName"": ""ОБОВ'ЯЗКОВО вкажи назву вулиці з локації"",
  ""latitude"": число або null,
  ""longitude"": число або null,
  ""aiMessage"": ""твоя відповідь користувачу""
}

Доступні категорії: Дороги, Освітлення, Сміття, Водопостачання, Громадський транспорт, Парки та зелені зони, Безпека, Шум, Тварини, Інше.
Доступні пріоритети: Низький, Середній, Високий, Критичний.

ПОПУЛЯРНІ ЛОКАЦІЇ ОСТРОГА:
1. Новий корпус Острозької академії (Монастир капуцинів) - вул. Семінарська, 2 - 50.32917, 26.51278
2. Старий корпус Острозької академії - вул. Семінарська, 2 - 50.32833, 26.51278
3. АТБ (супермаркет) - вул. Гальшки Острозької, 1в - 50.32729, 26.52463
4. Острозький замок - вул. Академічна, 5 - 50.32626, 26.52212
5. Нова Пошта - вул. Князів Острозьких, 3 - 50.32849, 26.51955
6. Укрпошта - просп. Незалежності, 7 - 50.32951, 26.52054
7. Татарська вежа - вул. Татарська - 50.3306, 26.5260
8. Автовокзал Острог - просп. Незалежності, 166 - 50.33557, 26.49400
9. Богоявленський собор - вул. Академічна, 5в - 50.32661, 26.52128
10. Гуртожиток Академічний дім - просп. Незалежності, 5 - 50.32951, 26.52054

Якщо згадано ""старий корпус"", ""академія"", ""університет"" - це Острозька академія (вул. Семінарська).

Якщо не можеш розпізнати голос - поверни JSON з порожніми полями та aiMessage з поясненням.

КРИТИЧНО ВАЖЛИВО:
1. Відповідай ТІЛЬКИ валідним JSON без додаткового тексту
2. Переконайся що JSON ПОВНИЙ і ЗАВЕРШЕНИЙ (всі дужки закриті)
3. Використовуй правильне UTF-8 кодування для українських символів
4. НЕ обрізай текст - завершуй всі поля повністю
5. Всі рядкові поля мають бути повністю заповнені без обривів";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new object[]
                        {
                            new { text = extractionPrompt },
                            new
                            {
                                inline_data = new
                                {
                                    mime_type = "audio/webm",
                                    data = base64Audio
                                }
                            }
                        }
                    }
                },
                generationConfig = new
                {
                    temperature = 0.3,
                    maxOutputTokens = 2048
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
                _logger.LogError("Gemini voice extraction error: {StatusCode} - {Content}", response.StatusCode, responseContent);
                return new ExtractedProblemData(
                    Title: "",
                    Description: "",
                    Categories: new List<string>(),
                    Priority: "Середній",
                    StreetName: null,
                    Latitude: null,
                    Longitude: null,
                    AiMessage: "Не вдалося розпізнати голос. Спробуйте говорити чіткіше.",
                    IsComplete: false
                );
            }

            var jsonResponse = JsonSerializer.Deserialize<GeminiResponse>(responseContent);
            var extractedText = jsonResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text?.Trim() ?? "";

            return ParseExtractedProblemData(extractedText, "");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error extracting problem data from voice");
            return new ExtractedProblemData(
                Title: "",
                Description: "",
                Categories: new List<string>(),
                Priority: "Середній",
                StreetName: null,
                Latitude: null,
                Longitude: null,
                AiMessage: "Помилка при обробці голосового повідомлення.",
                IsComplete: false
            );
        }
    }

    private string GetProblemExtractionPrompt(string userMessage)
    {
        return $@"Ти - AI асистент для створення звітів про міські проблеми в місті Острог, Рівненська область, Україна.
Користувач описує проблему. Витягни з опису інформацію та поверни у форматі JSON:

{{
  ""title"": ""короткий заголовок проблеми (до 100 символів)"",
  ""description"": ""детальний опис проблеми (мінімум 30 символів)"",
  ""categories"": [""категорія1""],
  ""priority"": ""пріоритет"",
  ""streetName"": ""ОБОВ'ЯЗКОВО вкажи назву вулиці з локації або найближчої відомої точки"",
  ""latitude"": число або null,
  ""longitude"": число або null,
  ""aiMessage"": ""твоя дружня відповідь користувачу про те що ти зрозумів""
}}

Доступні категорії: Дороги, Освітлення, Сміття, Водопостачання, Громадський транспорт, Парки та зелені зони, Безпека, Шум, Тварини, Інше.
Доступні пріоритети: Низький, Середній, Високий, Критичний.

ПОПУЛЯРНІ ЛОКАЦІЇ ОСТРОГА (використовуй для визначення координат та вулиць):
1. Новий корпус Острозької академії (Монастир капуцинів) - вул. Семінарська, 2 - координати: 50.32917, 26.51278
2. Старий корпус Острозької академії (головний корпус) - вул. Семінарська, 2 - координати: 50.32833, 26.51278
3. АТБ (супермаркет) - вул. Гальшки Острозької, 1в - координати: 50.32729, 26.52463
4. Острозький замок (Кругла вежа, Мурована вежа) - вул. Академічна, 5 - координати: 50.32626, 26.52212
5. Нова Пошта (відділення №1) - вул. Князів Острозьких, 3 - координати: 50.32849, 26.51955
6. Укрпошта (відділення 35800) - просп. Незалежності, 7 - координати: 50.32951, 26.52054
7. Татарська вежа - вул. Татарська - координати: 50.3306, 26.5260
8. Автовокзал Острог - просп. Незалежності, 166 - координати: 50.33557, 26.49400
9. Богоявленський собор (на території замку) - вул. Академічна, 5в - координати: 50.32661, 26.52128
10. Гуртожиток Академічний дім (№5) - просп. Незалежності, 5 - координати: 50.32951, 26.52054
11. Центр міста / Центральна площа - координати: 50.3290, 26.5180
12. Парк біля замку - вул. Академічна - координати: 50.3268, 26.5210
13. Ринок - просп. Незалежності - координати: 50.3295, 26.5190
14. Міська рада - просп. Незалежності, 12 - координати: 50.3292, 26.5175

Правила визначення пріоритету:
- Критичний: небезпека для життя, аварійні ситуації
- Високий: серйозні проблеми що потребують швидкого вирішення
- Середній: звичайні проблеми
- Низький: незначні косметичні проблеми

ВАЖЛИВО:
1. Якщо користувач згадує відому локацію з списку вище - ОБОВ'ЯЗКОВО встанови координати та streetName
2. Якщо згадано ""старий корпус"", ""академія"", ""університет"" - це Острозька академія
3. Вулицю завжди записуй у форматі ""вул. Назва"" або ""просп. Назва""
4. Якщо локація невідома - залиш координати null, але спробуй визначити вулицю за контекстом

Повідомлення користувача: ""{userMessage}""

КРИТИЧНО ВАЖЛИВО:
1. Відповідай ТІЛЬКИ валідним JSON без додаткового тексту
2. Переконайся що JSON ПОВНИЙ і ЗАВЕРШЕНИЙ (всі дужки закриті)
3. Використовуй правильне UTF-8 кодування для українських символів
4. НЕ обрізай текст - завершуй всі поля повністю
5. Всі рядкові поля мають бути повністю заповнені без обривів";
    }

    private ExtractedProblemData ParseExtractedProblemData(string jsonResponse, string originalMessage)
    {
        try
        {
            // Clean up the response - remove markdown code blocks if present
            var cleanJson = jsonResponse.Trim();
            
            _logger.LogInformation("Parsing JSON response (length: {Length}): {Response}", 
                cleanJson.Length, 
                cleanJson.Length > 500 ? cleanJson.Substring(0, 500) + "..." : cleanJson);
            
            if (cleanJson.StartsWith("```json"))
                cleanJson = cleanJson[7..];
            if (cleanJson.StartsWith("```"))
                cleanJson = cleanJson[3..];
            if (cleanJson.EndsWith("```"))
                cleanJson = cleanJson[..^3];
            cleanJson = cleanJson.Trim();

            // Try to fix incomplete JSON by checking if it ends properly
            if (!cleanJson.EndsWith("}"))
            {
                _logger.LogWarning("JSON response appears incomplete (doesn't end with }}). Attempting to fix...");
                
                // Check if response was truncated mid-string
                var lastQuoteIndex = cleanJson.LastIndexOf('"');
                var openQuotes = cleanJson.Count(c => c == '"');
                
                // If odd number of quotes, the string was cut off - close it
                if (openQuotes % 2 != 0)
                {
                    cleanJson += "\"";
                    _logger.LogInformation("Closed unclosed string");
                }
                
                // Try to close the JSON properly
                var openBraces = cleanJson.Count(c => c == '{');
                var closeBraces = cleanJson.Count(c => c == '}');
                if (openBraces > closeBraces)
                {
                    // Add missing closing braces
                    cleanJson += new string('}', openBraces - closeBraces);
                    _logger.LogInformation("Added {Count} closing braces", openBraces - closeBraces);
                }
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var parsed = JsonSerializer.Deserialize<ExtractedProblemJson>(cleanJson, options);

            if (parsed == null)
            {
                _logger.LogWarning("Parsed JSON is null");
                return new ExtractedProblemData(
                    Title: "",
                    Description: originalMessage,
                    Categories: new List<string>(),
                    Priority: "Середній",
                    StreetName: null,
                    Latitude: null,
                    Longitude: null,
                    AiMessage: "Не вдалося розпізнати дані. Опишіть проблему детальніше.",
                    IsComplete: false
                );
            }

            var isComplete = !string.IsNullOrWhiteSpace(parsed.Title) &&
                           !string.IsNullOrWhiteSpace(parsed.Description) &&
                           parsed.Description.Length >= 30 &&
                           parsed.Categories?.Count > 0;

            return new ExtractedProblemData(
                Title: parsed.Title ?? "",
                Description: parsed.Description ?? originalMessage,
                Categories: parsed.Categories ?? new List<string>(),
                Priority: parsed.Priority ?? "Середній",
                StreetName: parsed.StreetName,
                Latitude: parsed.Latitude,
                Longitude: parsed.Longitude,
                AiMessage: parsed.AiMessage ?? "Дані успішно розпізнано!",
                IsComplete: isComplete
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error parsing extracted problem data. Response length: {Length}, First 200 chars: {Preview}", 
                jsonResponse?.Length ?? 0, 
                jsonResponse?.Length > 200 ? jsonResponse.Substring(0, 200) : jsonResponse ?? "null");
            return new ExtractedProblemData(
                Title: "",
                Description: originalMessage,
                Categories: new List<string>(),
                Priority: "Середній",
                StreetName: null,
                Latitude: null,
                Longitude: null,
                AiMessage: "Виникла помилка при обробці. Спробуйте ще раз.",
                IsComplete: false
            );
        }
    }

    public async Task<ChatResponse> ProcessVoiceMessageAsync(Stream audioStream, string userRole, Guid? userId = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("Gemini API key is not configured");
            }

            // Read audio stream to byte array
            using var memoryStream = new MemoryStream();
            await audioStream.CopyToAsync(memoryStream, cancellationToken);
            var audioBytes = memoryStream.ToArray();
            var base64Audio = Convert.ToBase64String(audioBytes);

            // First, extract the user's intent/message from audio
            var extractUrl = $"https://generativelanguage.googleapis.com/v1beta/models/{GeminiFlashModel}:generateContent?key={apiKey}";

            var extractRequestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new object[]
                        {
                            new
                            {
                                text = @"Прослухай це голосове повідомлення українською мовою та витягни з нього запит користувача. 
Відповідай ТІЛЬКИ текстом того, що користувач просить або питає, без додаткових пояснень.
Збережи всю інформацію: адреси, категорії проблем, деталі.
Якщо не можеш розпізнати - відповідай 'НЕ_РОЗПІЗНАНО'."
                            },
                            new
                            {
                                inline_data = new
                                {
                                    mime_type = "audio/webm",
                                    data = base64Audio
                                }
                            }
                        }
                    }
                },
                generationConfig = new
                {
                    temperature = 0.3,
                    maxOutputTokens = 256
                }
            };

            var extractContent = new StringContent(
                JsonSerializer.Serialize(extractRequestBody),
                Encoding.UTF8,
                "application/json"
            );

            var extractResponse = await _httpClient.PostAsync(extractUrl, extractContent, cancellationToken);
            var extractResponseContent = await extractResponse.Content.ReadAsStringAsync(cancellationToken);

            if (!extractResponse.IsSuccessStatusCode)
            {
                _logger.LogError("Gemini voice processing error: {StatusCode} - {Content}", extractResponse.StatusCode, extractResponseContent);
                return new ChatResponse(
                    "Не вдалося розпізнати голос. Спробуйте говорити чіткіше або ближче до мікрофона.",
                    ChatResponseType.Error
                );
            }

            var extractJsonResponse = JsonSerializer.Deserialize<GeminiResponse>(extractResponseContent);
            var userMessage = extractJsonResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text?.Trim();

            if (string.IsNullOrEmpty(userMessage) || userMessage == "НЕ_РОЗПІЗНАНО")
            {
                return new ChatResponse(
                    "Не вдалося розпізнати голос. Спробуйте говорити чіткіше українською мовою.",
                    ChatResponseType.Error
                );
            }

            _logger.LogInformation("Voice message extracted: {Message}", userMessage);

            // Now process the extracted message as a regular chat request
            var chatRequest = new ChatRequest(userMessage, userRole, userId);
            return await ProcessChatMessageAsync(chatRequest, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing voice message with Gemini");
            return new ChatResponse(
                "Помилка при обробці голосового повідомлення. Спробуйте ще раз.",
                ChatResponseType.Error
            );
        }
    }

    private async Task<ChatIntent> ClassifyIntentAsync(string message, CancellationToken cancellationToken)
    {
        // First, check for keyword-based fallback patterns for analyze/compare intents
        // This ensures that even if the classifier fails, we can detect the intent
        var messageLower = message.ToLowerInvariant();
        var analyzeKeywords = new[] { "порівняй", "порівняння", "порівнян", "різниця", "різниц", "статистика", "аналіз", "найпопулярніш", "скільки проблем", "які найбільше", "в чому різниця", "відмінност", "чим відрізня", "що спільного", "схожість", "відмінність" };
        var isLikelyAnalyze = analyzeKeywords.Any(keyword => messageLower.Contains(keyword));
        
        // If keyword match is strong, return immediately to avoid misclassification
        if (isLikelyAnalyze)
        {
            _logger.LogInformation("Message: '{Message}' -> Classified as: AnalyzeProblems (keyword match)", message);
            return ChatIntent.AnalyzeProblems;
        }
        
        var prompt = $"""
                      Класифікуй намір користувача. Відповідай ТІЛЬКИ одним з цих слів (без додаткового тексту):

                      TOP_PROBLEMS - питає про топ/найкращі/найгірші проблеми за рейтингом
                      CATEGORY_PROBLEMS - питає про проблеми певної категорії
                      STATUS_PROBLEMS - питає про проблеми за статусом
                      PRIORITY_PROBLEMS - питає про проблеми за пріоритетом
                      RECENT_PROBLEMS - питає про останні/нещодавні проблеми
                      MY_PROBLEMS - питає про свої проблеми
                      ANALYZE_PROBLEMS - хоче ПОРІВНЯТИ, АНАЛІЗУВАТИ, дізнатись СТАТИСТИКУ, РІЗНИЦЮ між проблемами, питає "в чому різниця", "чим відрізняються"
                      HOW_TO_USE - питає як користуватися сайтом
                      WHAT_IS_SITE - питає що це за сайт/платформа
                      CONTACT_AUTHORS - хоче зв'язатися з авторами
                      ROLE_CAPABILITIES - питає про можливості своєї ролі
                      GENERAL - інші питання

                      Повідомлення: "{message}"
                      
                      Відповідь (ТІЛЬКИ одне слово):
                      """;

        var response = await CallGeminiAsyncExtended(prompt, GeminiLiteModel, cancellationToken, maxTokens: 32, temperature: 0.0);
        var rawResponse = response.Trim().ToUpperInvariant();
        
        // Clean up potential extra text from response
        var cleanedResponse = rawResponse.Split('\n')[0].Trim();
        if (cleanedResponse.Contains(" "))
            cleanedResponse = cleanedResponse.Split(' ')[0];
        
        var intent = cleanedResponse switch
        {
            "TOP_PROBLEMS" => ChatIntent.GetTopProblems,
            "CATEGORY_PROBLEMS" => ChatIntent.GetProblemsByCategory,
            "STATUS_PROBLEMS" => ChatIntent.GetProblemsByStatus,
            "PRIORITY_PROBLEMS" => ChatIntent.GetProblemsByPriority,
            "RECENT_PROBLEMS" => ChatIntent.GetRecentProblems,
            "MY_PROBLEMS" => ChatIntent.GetMyProblems,
            "ANALYZE_PROBLEMS" => ChatIntent.AnalyzeProblems,
            "HOW_TO_USE" => ChatIntent.HowToUse,
            "WHAT_IS_SITE" => ChatIntent.WhatIsThisSite,
            "CONTACT_AUTHORS" => ChatIntent.ContactAuthors,
            "ROLE_CAPABILITIES" => ChatIntent.RoleCapabilities,
            _ => ChatIntent.GeneralQuestion
        };

        _logger.LogInformation("Message: '{Message}' -> Classified as: {Intent} (raw response: '{RawResponse}')", 
            message, intent, rawResponse);
        
        return intent;
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
        if (!request.UserId.HasValue || request.UserRole == "Guest")
        {
            return new ChatResponse(
                "Щоб переглянути свої подані проблеми, вам потрібно увійти в систему. 🔐\n\n" +
                "Після входу ви зможете:\n" +
                "- Переглядати всі свої звіти\n" +
                "- Відстежувати статус вирішення\n" +
                "- Редагувати та доповнювати свої проблеми\n" +
                "- Отримувати сповіщення про оновлення\n\n" +
                "Натисніть кнопку 'Увійти' у верхньому правому куті.",
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
            ? $"Ось ваші подані проблеми (показано {problemSummaries.Count} з {userProblems.Count}):"
            : "Ви ще не створювали жодної проблеми.";

        return new ChatResponse(message, ChatResponseType.ProblemsList, problemSummaries);
    }

    private async Task<ChatResponse> HandleAnalyzeProblemsAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        // Get all problems with ratings for analysis
        var allProblems = await _problemQueries.GetAll(cancellationToken);
        var problemsWithRatings = new List<(Problem Problem, double AvgRating, int CommentsCount)>();

        foreach (var problem in allProblems)
        {
            var ratings = await _ratingQueries.GetByProblemId(problem.Id, cancellationToken);
            var avgRating = ratings.Any() ? ratings.Average(r => r.Points) : 0;
            problemsWithRatings.Add((problem, avgRating, problem.Comments.Count));
        }

        // Prepare data summary for AI analysis
        var topByRating = problemsWithRatings
            .OrderByDescending(p => p.AvgRating)
            .Take(10)
            .ToList();

        var categoriesStats = allProblems
            .SelectMany(p => p.Categories)
            .GroupBy(c => c.Value)
            .Select(g => new { Category = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ToList();

        var statusStats = allProblems
            .GroupBy(p => p.Status.Value)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToList();

        var priorityStats = allProblems
            .GroupBy(p => p.Priority.Value)
            .Select(g => new { Priority = g.Key, Count = g.Count() })
            .ToList();

        // Build context for AI
        var dataContext = new StringBuilder();
        dataContext.AppendLine("СТАТИСТИКА СИСТЕМИ:");
        dataContext.AppendLine($"Всього проблем: {allProblems.Count}");
        dataContext.AppendLine();

        dataContext.AppendLine("ТОП-10 ПРОБЛЕМ ЗА РЕЙТИНГОМ:");
        foreach (var (problem, rating, comments) in topByRating)
        {
            dataContext.AppendLine($"- \"{problem.Title}\" (Рейтинг: {rating:F1}, Коментарів: {comments}, Статус: {problem.Status.Value}, Пріоритет: {problem.Priority.Value}, Категорії: {string.Join(", ", problem.Categories.Select(c => c.Value))})");
        }
        dataContext.AppendLine();

        dataContext.AppendLine("СТАТИСТИКА ЗА КАТЕГОРІЯМИ:");
        foreach (var stat in categoriesStats)
        {
            dataContext.AppendLine($"- {stat.Category}: {stat.Count} проблем");
        }
        dataContext.AppendLine();

        dataContext.AppendLine("СТАТИСТИКА ЗА СТАТУСАМИ:");
        foreach (var stat in statusStats)
        {
            dataContext.AppendLine($"- {stat.Status}: {stat.Count} проблем");
        }
        dataContext.AppendLine();

        dataContext.AppendLine("СТАТИСТИКА ЗА ПРІОРИТЕТАМИ:");
        foreach (var stat in priorityStats)
        {
            dataContext.AppendLine($"- {stat.Priority}: {stat.Count} проблем");
        }

        var systemPrompt = GetSystemPrompt(request.UserRole);
        var prompt = $"""
                      {systemPrompt}

                      Користувач просить проаналізувати дані про проблеми міста.

                      ЗАПИТ КОРИСТУВАЧА: "{request.Message}"

                      ДАНІ ДЛЯ АНАЛІЗУ:
                      {dataContext}

                      ІНСТРУКЦІЇ:
                      1. Проаналізуй дані та дай детальну, інформативну відповідь на запит користувача
                      2. Якщо користувач просить порівняти проблеми - порівняй топ-2 за рейтингом та поясни різницю детально:
                         - Опиши кожну проблему окремо
                         - Порівняй їх за категоріями, пріоритетом, статусом
                         - Порівняй рейтинги та кількість коментарів
                         - Поясни чому одна популярніша за іншу
                         - Зроби висновки про те, що це говорить про пріоритети громади
                      3. Використовуй конкретні цифри та факти з даних
                      4. Будь аналітичним та об'єктивним
                      5. Якщо є тренди або цікаві спостереження - обов'язково згадай їх
                      6. Відповідай українською мовою
                      7. Структуруй відповідь для зручності читання (використовуй списки, абзаци, заголовки)
                      8. Дай практичні висновки або рекомендації якщо доречно
                      9. Використовуй емодзі для виразності (але помірно)

                      Дай ПОВНУ та ДЕТАЛЬНУ відповідь (мінімум 200-300 слів). НЕ ОБРІЗАЙ відповідь - завершуй всі думки повністю.
                      """;

        var response = await CallGeminiAsyncExtended(prompt, GeminiFlashModel, cancellationToken, maxTokens: 4096, temperature: 0.7);
        return new ChatResponse(response, ChatResponseType.Text);
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
                 4. Відстежуйте статус у розділі 'Подані проблеми'
                 """
        };

        var message = $"""
                       # Як користуватися платформою Острог Разом

                                   {roleSpecificInstructions}

                                   **Потрібна допомога?** Запитайте мене про будь-що!
                       """;

        return Task.FromResult(new ChatResponse(message, ChatResponseType.Help));
    }

    private async Task<ChatResponse> HandleWhatIsThisSiteAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var systemPrompt = GetSystemPrompt(request.UserRole);
        var prompt = $"""
                      {systemPrompt}

                      Користувач запитує: "{request.Message}"

                      Розкажи про платформу "Острог Разом" цікаво та по-різному кожного разу. Використовуй наступну інформацію як базу, але додай свої думки та приклади:

                      БАЗОВА ІНФОРМАЦІЯ:
                      - Це веб-платформа для звітування та вирішення міських проблем в Острозі
                      - Основні можливості: інтерактивна карта, звітування з фото, відстеження статусу, коментарі, оцінки
                      - Категорії: Дороги, Освітлення, Сміття, Водопостачання, Транспорт, Парки, Безпека, Шум, Тварини
                      - Ролі: Користувач (створює звіти), Координатор (вирішує проблеми), Адміністратор (керує системою)

                      ВАЖЛИВО:
                      1. Кожна відповідь має бути унікальною та цікавою
                      2. Додай конкретні приклади як платформа допомагає різним групам:
                         - Простим людям міста (швидко повідомити про яму, зламане освітлення)
                         - Депутатам (бачити реальні проблеми громади, приймати обґрунтовані рішення)
                         - Волонтерам (координувати зусилля, відстежувати прогрес)
                      3. Використовуй живу мову, а не шаблони
                      4. Можеш додати емодзі для виразності, але помірно
                      5. Зроби відповідь особистою та залучаючою
                      6. ОБОВ'ЯЗКОВО дай ПОВНУ та ДЕТАЛЬНУ відповідь (мінімум 200-300 слів)

                      Дай відповідь українською мовою.
                      """;

        var response = await CallGeminiAsyncExtended(prompt, GeminiFlashModel, cancellationToken, maxTokens: 2048);
        return new ChatResponse(response, ChatResponseType.Help);
    }

    private Task<ChatResponse> HandleContactAuthorsAsync(ChatRequest request, CancellationToken cancellationToken)
    {
        var message = """
                      # Зв'язок з авторами

                      **Острог Разом** — це демо-проект, створений для демонстрації можливостей сучасних веб-технологій.

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
            "Guest" => """
                       # Можливості для гостей

                       Ви переглядаєте сайт як **гість** (без входу в систему).

                       ## Що ви можете робити зараз:
                       - ✅ Переглядати карту з усіма проблемами міста
                       - ✅ Читати опис проблем та коментарі
                       - ✅ Бачити статуси та пріоритети проблем
                       - ✅ Використовувати AI-асистента для пошуку інформації

                       ## Що стане доступним після входу:
                       - 🔓 Створення власних звітів про проблеми
                       - 🔓 Додавання фотографій та коментарів
                       - 🔓 Оцінювання вирішених проблем
                       - 🔓 Відстеження статусу ваших звітів
                       - 🔓 Отримання сповіщень про оновлення

                       **Увійдіть в систему**, щоб отримати повний доступ до функціоналу платформи! 🚀
                       """,

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
        // Check if this is actually an analytical question that was misclassified
        var messageLower = request.Message.ToLowerInvariant();
        var analyzeIndicators = new[] { "порівня", "різниц", "статистик", "аналіз", "найпопулярн", "скільки", "найбільш", "відмінн", "схож" };
        var isLikelyAnalytical = analyzeIndicators.Any(indicator => messageLower.Contains(indicator));
        
        if (isLikelyAnalytical)
        {
            _logger.LogInformation("GeneralQuestion appears to be analytical, redirecting to HandleAnalyzeProblemsAsync");
            return await HandleAnalyzeProblemsAsync(request, cancellationToken);
        }
        
        var systemPrompt = GetSystemPrompt(request.UserRole);
        var prompt = $"""
                      {systemPrompt}

                                  Питання користувача: {request.Message}

                                  Дай корисну та дружню відповідь українською мовою. Якщо питання стосується проблем міста, запропонуй конкретні дії.
                                  
                                  ВАЖЛИВО: Дай ПОВНУ відповідь (мінімум 100-150 слів).
                      """;

        var response = await CallGeminiAsyncExtended(prompt, GeminiFlashModel, cancellationToken, maxTokens: 2048, temperature: 0.9);
        return new ChatResponse(response, ChatResponseType.Text);
    }

    private async Task<string> CallGeminiAsync(string prompt, string model, CancellationToken cancellationToken)
    {
        return await CallGeminiAsyncExtended(prompt, model, cancellationToken, maxTokens: 1024);
    }

    private async Task<string> CallGeminiAsyncExtended(string prompt, string model, CancellationToken cancellationToken, int maxTokens = 1024, double temperature = 0.9)
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
                temperature,
                maxOutputTokens = maxTokens,
                topP = 0.95
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
                Ти - AI асистент платформи Острог Разом для вирішення міських проблем в місті Острог.

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
                            2. Будь дружнім, корисним та креативним
                            3. Якщо не знаєш відповіді - скажи про це чесно
                            4. Пропонуй конкретні дії для вирішення питань
                            5. Кожна відповідь має бути унікальною - уникай шаблонних фраз
                            6. Додавай конкретні приклади та сценарії використання
                            7. Використовуй живу розмовну мову, а не формальні заготовки
                            8. Можеш використовувати емодзі для виразності, але помірно
                            9. Це демо-проект, тому деякі дані є ілюстративними
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
    AnalyzeProblems,
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

internal class ExtractedProblemJson
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("categories")]
    public List<string>? Categories { get; set; }

    [JsonPropertyName("priority")]
    public string? Priority { get; set; }

    [JsonPropertyName("streetName")]
    public string? StreetName { get; set; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }

    [JsonPropertyName("aiMessage")]
    public string? AiMessage { get; set; }
}
