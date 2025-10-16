using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Comments.Exceptions;
using Domain.Comments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Application.Comments.Commands;

public record DeleteCommentCommand : IRequest<Result<Comment, CommentException>>
{
    public required Guid CommentId { get; init; }
}

public class DeleteCommentCommandHandler(
    ICommentRepository commentRepository)
    : IRequestHandler<DeleteCommentCommand, Result<Comment, CommentException>>
{
    public async Task<Result<Comment, CommentException>> Handle(
        DeleteCommentCommand request,
        CancellationToken cancellationToken)
    {
        var commentId = new CommentId(request.CommentId);
        var existingComment = await commentRepository.GetById(commentId, cancellationToken);

        return await existingComment.Match<Task<Result<Comment, CommentException>>>(
            async comment => await DeleteEntity(comment, cancellationToken),
            () => Task.FromResult<Result<Comment, CommentException>>(
                new CommentNotFoundException(commentId))
        );
    }

    private async Task<Result<Comment, CommentException>> DeleteEntity(
        Comment comment,
        CancellationToken cancellationToken)
    {
        try
        {
            return await commentRepository.Delete(comment, cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503")
        {
            return new CommentHasRelatedEntitiesException(comment.Id);
        }
        catch (Exception exception)
        {
            return new CommentUnknownException(comment.Id, exception);
        }
    }
}