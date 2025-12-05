using Application.Comments.Exceptions;
using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Services.SignalRService;
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
    private readonly ISignalRService _signalRService;
    private readonly IProblemQueries _problemQueries;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        IHttpContextAccessor httpContextAccessor,
        ISignalRService signalRService,
        IProblemQueries problemQueries)
    {
        _commentRepository = commentRepository;
        _httpContextAccessor = httpContextAccessor;
        _signalRService = signalRService;
        _problemQueries = problemQueries;
    }

    public async Task<Result<Comment, CommentException>> Handle(
        CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var commentId = CommentId.New();
        var problemId = new ProblemId(request.ProblemId);

        try
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "id");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userIdGuid))
            {
                return new UserIdNotFoundException(commentId);
            }

            var userId = new UserId(userIdGuid);

            var comment = Comment.New(commentId, request.Content, problemId, userId);

            var result = await _commentRepository.Add(comment, cancellationToken);
            
            var commentDto = CommentNotificationDto.FromDomainModel(result);
            await _signalRService.SendCommentToGroup(problemId, commentDto, cancellationToken);

            var problemOption = await _problemQueries.GetById(problemId, cancellationToken);
            if (problemOption.HasValue)
            {
                var p = problemOption.ValueOr(default(Problem)!);
                if (p is not null && p.CoordinatorId!.Value != userIdGuid)
                {
                    var commenterName = comment.User != null
                        ? $"{comment.User.FullName}"
                        : "Хтось";
                    
                    var notification = NotificationDto.Create(
                        "comment",
                        $"{commenterName} додав коментар до вашої проблеми",
                        p.Id.Value.ToString(),
                        p.Title);
                    
                    await _signalRService.SendNotificationToUser(
                        p.CoordinatorId,
                        notification,
                        cancellationToken);
                }
            }

            return result;
        }
        catch (Exception exception)
        {
            return new CommentUnknownException(commentId, exception);
        }
    }
}