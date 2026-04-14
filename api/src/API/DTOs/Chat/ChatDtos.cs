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

// Admin Dashboard DTOs
public record AdminChatMessageRequest(
    string Message
);

public record AdminChatMessageResponse(
    string Message,
    string ResponseType,
    DashboardFilterDto? SuggestedFilter = null,
    List<ProblemSummaryDto>? Problems = null
);

public record DashboardFilterDto(
    string? Status,
    string? Category,
    string? Priority,
    string? DateRange,
    string? SortBy
);

public record DashboardStatisticsResponse(
    int TotalProblems,
    int NewProblems,
    int InProgressProblems,
    int CompletedProblems,
    int RejectedProblems,
    int TotalUsers,
    int TotalComments,
    double AverageRating,
    int CriticalProblems,
    int HighPriorityProblems,
    int ProblemsThisMonth,
    int ProblemsLastMonth,
    double ResolutionRate,
    double AvgResolutionTimeDays,
    List<CategoryStatDto> CategoryStats,
    List<StatusStatDto> StatusStats,
    List<PriorityStatDto> PriorityStats,
    List<MonthlyTrendDto> MonthlyTrends,
    List<ProblemSummaryDto> RecentProblems,
    List<ProblemSummaryDto> TopRatedProblems
);

public record CategoryStatDto(string Category, int Count, double Percentage);
public record StatusStatDto(string Status, int Count, double Percentage);
public record PriorityStatDto(string Priority, int Count, double Percentage);
public record MonthlyTrendDto(string Month, int Created, int Resolved);
