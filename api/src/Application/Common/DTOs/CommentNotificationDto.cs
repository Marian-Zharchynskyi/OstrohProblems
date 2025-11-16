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
        if (comment.User == null)
        {
            throw new InvalidOperationException("Comment must have a user for notification");
        }

        return new CommentNotificationDto(
            comment.Id.Value,
            comment.Content,
            comment.ProblemId.Value,
            comment.User.Id.Value,
            comment.User.FullName!,
            comment.CreatedAt
        );
    }
}
