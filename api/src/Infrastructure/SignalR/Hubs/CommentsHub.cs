using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Domain.Identity.Roles;

namespace Infrastructure.SignalR.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = $"{RoleNames.Admin}, {RoleNames.User}")]
public class CommentsHub : Hub
{
    public async Task JoinProblemGroup(string problemId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"problem_{problemId}");
    }

    public async Task LeaveProblemGroup(string problemId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"problem_{problemId}");
    }

    public async Task SendCommentToProblem(string problemId, object comment)
    {
        await Clients.Group($"problem_{problemId}").SendAsync("ReceiveComment", comment);
    }
}
