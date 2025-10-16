using Application.Comments.Exceptions;
using Application.Common;
using Application.Common.Interfaces.Repositories;
using Domain.Comments;
using Domain.Identity.Users;
using Domain.Problems;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Comments.Commands;

public class CreateCommentCommand : IRequest<Result<Comment, CommentException>>
{
    public required string Content { get; init; }
    public required Guid ProblemId { get; init; }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result<Comment, CommentException>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _commentRepository = commentRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<Comment, CommentException>> Handle(
        CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var commentId = CommentId.New();
        var problemId = new ProblemId(request.ProblemId);

        try
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?
                .Claims.FirstOrDefault(c => c.Type == "id");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userIdGuid))
            {
                return new UserIdNotFoundException(commentId);
            }

            var userId = new UserId(userIdGuid);

            var comment = Comment.New(commentId, request.Content, problemId, userId);

            return await _commentRepository.Add(comment, cancellationToken);
        }
        catch (Exception exception)
        {
            return new CommentUnknownException(commentId, exception);
        }
    }
}
