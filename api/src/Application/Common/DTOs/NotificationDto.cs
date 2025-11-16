namespace Application.Common.DTOs;

public record NotificationDto(
    string Id,
    string Type,
    string Message,
    string ProblemId,
    string ProblemTitle,
    DateTime CreatedAt)
{
    public static NotificationDto Create(
        string type,
        string message,
        string problemId,
        string problemTitle)
    {
        return new NotificationDto(
            Guid.NewGuid().ToString(),
            type,
            message,
            problemId,
            problemTitle,
            DateTime.UtcNow
        );
    }
}
