namespace Application.Services.GeminiService;

public interface IGeminiService
{
    Task<ChatResponse> ProcessChatMessageAsync(ChatRequest request, CancellationToken cancellationToken = default);
    Task<string> TranscribeAudioAsync(Stream audioStream, CancellationToken cancellationToken = default);
    Task<ChatResponse> ProcessVoiceMessageAsync(Stream audioStream, string userRole, Guid? userId = null, CancellationToken cancellationToken = default);
    Task<ExtractedProblemData> ExtractProblemDataAsync(string userMessage, CancellationToken cancellationToken = default);
    Task<ExtractedProblemData> ExtractProblemDataFromVoiceAsync(Stream audioStream, CancellationToken cancellationToken = default);
    Task<DashboardStatistics> GetDashboardStatisticsAsync(CancellationToken cancellationToken = default);
    Task<AdminChatResponse> ProcessAdminChatMessageAsync(string message, CancellationToken cancellationToken = default);
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
    Error,
    DashboardUpdate
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

public record ExtractedProblemData(
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

// Dashboard Statistics models
public record DashboardStatistics(
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
    List<CategoryStat> CategoryStats,
    List<StatusStat> StatusStats,
    List<PriorityStat> PriorityStats,
    List<MonthlyTrend> MonthlyTrends,
    List<ProblemSummaryForChat> RecentProblems,
    List<ProblemSummaryForChat> TopRatedProblems
);

public record CategoryStat(string Category, int Count, double Percentage);
public record StatusStat(string Status, int Count, double Percentage);
public record PriorityStat(string Priority, int Count, double Percentage);
public record MonthlyTrend(string Month, int Created, int Resolved);

public record AdminChatResponse(
    string Message,
    ChatResponseType ResponseType,
    DashboardFilter? SuggestedFilter = null,
    List<ProblemSummaryForChat>? Problems = null
);

public record DashboardFilter(
    string? Status,
    string? Category,
    string? Priority,
    string? DateRange,
    string? SortBy
);
