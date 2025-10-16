using API.DTOs.Users;
using Domain.Comments;

namespace API.DTOs.Comments;

public record CommentDto(
    Guid? Id,
    string Content,
    Guid ProblemId,
    UserDto? User,
    DateTime CreatedAt,
    DateTime UpdatedAt)
{
    public static CommentDto FromDomainModel(Comment comment)
        => new(
            comment.Id.Value,
            comment.Content,
            comment.ProblemId.Value,
            comment.User == null ? null : UserDto.FromDomainModel(comment.User),
            comment.CreatedAt,
            comment.UpdatedAt
        );
}