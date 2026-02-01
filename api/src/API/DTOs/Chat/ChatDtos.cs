namespace API.DTOs.Chat;

public record ChatMessageRequest(
    string Message
);

public record ChatMessageResponse(
    string Message,
    string ResponseType,
    List<ProblemSummaryDto>? Problems = null
);

public record ProblemSummaryDto(
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

public record TranscribeAudioResponse(
    string Transcription
);

public record ExtractProblemRequest(
    string Message
);

public record ExtractedProblemResponse(
    string Title,
    string Description,
    List<string> Categories,
    string Priority,
    string? StreetName,
    double? Latitude,
    double? Longitude,
    string? AiMessage,
    bool IsComplete
);
