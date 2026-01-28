using API.DTOs.Chat;
using Application.Common.Interfaces;
using Application.Services.GeminiService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("chat")]
[ApiController]
public class ChatController(IGeminiService geminiService, IIdentityService identityService) : ControllerBase
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

    [AllowAnonymous]
    [HttpPost("transcribe")]
    public async Task<ActionResult<TranscribeAudioResponse>> TranscribeAudio(
        IFormFile audioFile,
        CancellationToken cancellationToken)
    {
        if (audioFile == null || audioFile.Length == 0)
        {
            return BadRequest(new { error = "Audio file is required" });
        }

        // Validate file size (max 10MB)
        if (audioFile.Length > 10 * 1024 * 1024)
        {
            return BadRequest(new { error = "Audio file is too large. Maximum size is 10MB." });
        }

        using var stream = audioFile.OpenReadStream();
        var transcription = await geminiService.TranscribeAudioAsync(stream, cancellationToken);

        return new TranscribeAudioResponse(transcription);
    }

    [AllowAnonymous]
    [HttpPost("voice-message")]
    public async Task<ActionResult<ChatMessageResponse>> ProcessVoiceMessage(
        IFormFile audioFile,
        CancellationToken cancellationToken)
    {
        if (audioFile == null || audioFile.Length == 0)
        {
            return BadRequest(new { error = "Audio file is required" });
        }

        // Validate file size (max 10MB)
        if (audioFile.Length > 10 * 1024 * 1024)
        {
            return BadRequest(new { error = "Audio file is too large. Maximum size is 10MB." });
        }

        var userRole = GetUserRole();
        var userId = GetUserId();

        using var stream = audioFile.OpenReadStream();
        var response = await geminiService.ProcessVoiceMessageAsync(stream, userRole, userId, cancellationToken);

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
        return identityService.GetUserRoles()
            .Match(
                roles => roles.FirstOrDefault() ?? "Guest",
                () => "Guest"
            );
    }

    private Guid? GetUserId()
    {
        return identityService.GetUserId()
            .Match(
                userId => (Guid?)userId.Value,
                () => null
            );
    }
}
