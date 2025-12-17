using Application.Common.DTOs;
using Domain.Identity.Users;
using Domain.Problems;

namespace Application.Services.SignalRService;

public interface ISignalRService
{
    Task SendCommentToGroup(ProblemId problemId, CommentNotificationDto comment, CancellationToken cancellationToken = default);
    Task SendNotificationToUser(UserId userId, NotificationDto notification, CancellationToken cancellationToken = default);
    Task SendRefreshToAll(CancellationToken cancellationToken = default);
}
