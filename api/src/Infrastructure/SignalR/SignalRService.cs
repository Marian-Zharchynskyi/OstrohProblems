using Application.Common.DTOs;
using Application.Services.SignalRService;
using Domain.Identity.Users;
using Domain.Problems;
using Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

public class SignalRService : ISignalRService
{
    private readonly IHubContext<CommentsHub> _commentsHubContext;
    private readonly IHubContext<NotificationsHub> _notificationsHubContext;

    public SignalRService(
        IHubContext<CommentsHub> commentsHubContext,
        IHubContext<NotificationsHub> notificationsHubContext)
    {
        _commentsHubContext = commentsHubContext;
        _notificationsHubContext = notificationsHubContext;
    }

    public async Task SendCommentToGroup(ProblemId problemId, CommentNotificationDto comment, CancellationToken cancellationToken = default)
    {
        await _commentsHubContext.Clients
            .Group($"problem_{problemId}")
            .SendAsync("ReceiveComment", comment, cancellationToken);
    }

    public async Task SendNotificationToUser(UserId userId, NotificationDto notification, CancellationToken cancellationToken = default)
    {
        await _notificationsHubContext.Clients
            .Group($"user_{userId}")
            .SendAsync("ReceiveNotification", notification, cancellationToken);
    }
}
