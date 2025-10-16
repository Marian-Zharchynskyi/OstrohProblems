using Domain.Comments;

namespace API.DTOs.Comments;

public record CreateCommentDto(string Content, Guid ProblemId)
{
    public static CreateCommentDto FromDomainModel(Comment comment)
        => new(
            comment.Content,
            comment.ProblemId.Value
        );
};