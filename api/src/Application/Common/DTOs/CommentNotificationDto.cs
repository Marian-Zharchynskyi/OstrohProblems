using Domain.Comments;

namespace Application.Common.DTOs;

public record CommentNotificationDto(
    Guid Id,
    string Content,
    Guid ProblemId,
    Guid UserId,
    string FullName,
    DateTime CreatedAt)
{
    public static CommentNotificationDto FromDomainModel(Comment comment)
    {
        var userId = comment.User?.Id.Value ?? comment.UserId.Value;
        var fullName = comment.User?.FullName ?? "Хтось";

        return new CommentNotificationDto(
            comment.Id.Value,
            comment.Content,
            comment.ProblemId.Value,
            userId,
            fullName,
            comment.CreatedAt
        );
    }
}
