using API.DTOs.Chat;
using Application.Services.GeminiService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Route("chat")]
[ApiController]
public class ChatController(IGeminiService geminiService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("message")]
    public async Task<ActionResult<ChatMessageResponse>> SendMessage(
        [FromBody] ChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        var userRole = GetUserRole();
        var userId = GetUserId();

        var chatRequest = new ChatRequest(
            request.Message,
            userRole,
            userId
        );

        var response = await geminiService.ProcessChatMessageAsync(chatRequest, cancellationToken);

        return new ChatMessageResponse(
            response.Message,
            response.ResponseType.ToString(),
            response.Problems?.Select(p => new ProblemSummaryDto(
                p.Id,
                p.Title,
                p.Description,
                p.Status,
                p.Priority,
                p.Latitude,
                p.Longitude,
                p.Categories,
                p.AverageRating,
                p.CommentsCount,
                p.CreatedAt,
                p.CreatorName
            )).ToList()
        );
    }

    private string GetUserRole()
    {
        var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
        return roleClaim ?? "Guest";
    }

    private Guid? GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userIdClaim, out var userId))
            return userId;
        return null;
    }
}
