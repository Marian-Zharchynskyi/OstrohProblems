using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Domain.Identity.Roles;

namespace Infrastructure.SignalR.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}, {RoleNames.Coordinator}")]
public class NotificationsHub : Hub
{
    public async Task JoinUserGroup(string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"user_{userId}");
    }

    public async Task LeaveUserGroup(string userId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"user_{userId}");
    }

    public async Task SendNotificationToUser(string userId, object notification)
    {
        await Clients.Group($"user_{userId}").SendAsync("ReceiveNotification", notification);
    }
}
