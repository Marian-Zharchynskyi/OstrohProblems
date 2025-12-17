namespace API.DTOs.Notifications;

public record NotificationDto
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string Type { get; init; } = string.Empty; // "comment", "status_change", "reply"
    public string Message { get; init; } = string.Empty;
    public string ProblemId { get; init; } = string.Empty;
    public string ProblemTitle { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public bool IsRead { get; init; } = false;
}
