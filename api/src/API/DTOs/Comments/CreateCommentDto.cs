using Domain.Comments;

namespace API.DTOs.Comments;

public record CreateCommentDto(Guid? Id, string Content, Guid ProblemId)
{
    public static CreateCommentDto FromDomainModel(Comment comment)
        => new(
            comment.Id.Value,
            comment.Content,
            comment.ProblemId.Value
        );
};