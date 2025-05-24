using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Comments.Exceptions;
using Domain.Comments;
using MediatR;

namespace Application.Comments.Commands;

public record UpdateCommentCommand : IRequest<Result<Comment, CommentException>>
{
    public required Guid CommentId { get; init; }
    public required string Content { get; init; }
}

public class UpdateCommentCommandHandler(
    ICommentRepository commentRepository)
    : IRequestHandler<UpdateCommentCommand, Result<Comment, CommentException>>
{
    public async Task<Result<Comment, CommentException>> Handle(
        UpdateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var commentId = new CommentId(request.CommentId);
        var existingComment = await commentRepository.GetById(commentId, cancellationToken);

        return await existingComment.Match<Task<Result<Comment, CommentException>>>(
            async comment => await UpdateComment(comment, request.Content, cancellationToken),
            () => Task.FromResult<Result<Comment, CommentException>>(
                new CommentNotFoundException(commentId))
        );
    }

    private async Task<Result<Comment, CommentException>> UpdateComment(
        Comment comment,
        string content,
        CancellationToken cancellationToken)
    {
        try
        {
            comment.UpdateContent(content);
            return await commentRepository.Update(comment, cancellationToken);
        }
        catch (Exception exception)
        {
            return new CommentUnknownException(comment.Id, exception);
        }
    }
}