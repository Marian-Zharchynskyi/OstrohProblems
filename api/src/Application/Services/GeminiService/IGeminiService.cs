namespace Application.Services.GeminiService;

public interface IGeminiService
{
    Task<ChatResponse> ProcessChatMessageAsync(ChatRequest request, CancellationToken cancellationToken = default);
    Task<string> TranscribeAudioAsync(Stream audioStream, CancellationToken cancellationToken = default);
    Task<ChatResponse> ProcessVoiceMessageAsync(Stream audioStream, string userRole, Guid? userId = null, CancellationToken cancellationToken = default);
}

public record ChatRequest(
    string Message,
    string UserRole,
    Guid? UserId = null
);

public record ChatResponse(
    string Message,
    ChatResponseType ResponseType,
    List<ProblemSummaryForChat>? Problems = null
);

public enum ChatResponseType
{
    Text,
    ProblemsList,
    SingleProblem,
    Help,
    Error
}

public record ProblemSummaryForChat(
    Guid Id,
    string Title,
    string Description,
    string Status,
    string Priority,
    double Latitude,
    double Longitude,
    List<string> Categories,
    double? AverageRating,
    int CommentsCount,
    DateTime CreatedAt,
    string? CreatorName
);
