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
        var userIdOption = identityService.GetUserId();
        return userIdOption.IsSome
            ? userIdOption.Match(
                userId => (Guid?)userId.Value,
                () => null
            )
            : null;
    }

    [Authorize]
    [HttpPost("extract-problem")]
    public async Task<ActionResult<ExtractedProblemResponse>> ExtractProblemData(
        [FromBody] ExtractProblemRequest request,
        CancellationToken cancellationToken)
    {
        var result = await geminiService.ExtractProblemDataAsync(request.Message, cancellationToken);

        return new ExtractedProblemResponse(
            result.Title,
            result.Description,
            result.Categories,
            result.Priority,
            result.StreetName,
            result.Latitude,
            result.Longitude,
            result.AiMessage,
            result.IsComplete
        );
    }

    [Authorize]
    [HttpPost("extract-problem-voice")]
    public async Task<ActionResult<ExtractedProblemResponse>> ExtractProblemDataFromVoice(
        IFormFile audioFile,
        CancellationToken cancellationToken)
    {
        if (audioFile == null || audioFile.Length == 0)
        {
            return BadRequest(new { error = "Audio file is required" });
        }

        if (audioFile.Length > 10 * 1024 * 1024)
        {
            return BadRequest(new { error = "Audio file is too large. Maximum size is 10MB." });
        }

        using var stream = audioFile.OpenReadStream();
        var result = await geminiService.ExtractProblemDataFromVoiceAsync(stream, cancellationToken);

        return new ExtractedProblemResponse(
            result.Title,
            result.Description,
            result.Categories,
            result.Priority,
            result.StreetName,
            result.Latitude,
            result.Longitude,
            result.AiMessage,
            result.IsComplete
        );
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("admin/statistics")]
    public async Task<ActionResult<DashboardStatisticsResponse>> GetDashboardStatistics(
        CancellationToken cancellationToken)
    {
        var stats = await geminiService.GetDashboardStatisticsAsync(cancellationToken);

        return new DashboardStatisticsResponse(
            stats.TotalProblems,
            stats.NewProblems,
            stats.InProgressProblems,
            stats.CompletedProblems,
            stats.RejectedProblems,
            stats.TotalUsers,
            stats.TotalComments,
            stats.AverageRating,
            stats.CriticalProblems,
            stats.HighPriorityProblems,
            stats.ProblemsThisMonth,
            stats.ProblemsLastMonth,
            stats.ResolutionRate,
            stats.AvgResolutionTimeDays,
            stats.CategoryStats.Select(c => new CategoryStatDto(c.Category, c.Count, c.Percentage)).ToList(),
            stats.StatusStats.Select(s => new StatusStatDto(s.Status, s.Count, s.Percentage)).ToList(),
            stats.PriorityStats.Select(p => new PriorityStatDto(p.Priority, p.Count, p.Percentage)).ToList(),
            stats.MonthlyTrends.Select(t => new MonthlyTrendDto(t.Month, t.Created, t.Resolved)).ToList(),
            stats.RecentProblems.Select(p => new ProblemSummaryDto(
                p.Id, p.Title, p.Description, p.Status, p.Priority,
                p.Latitude, p.Longitude, p.Categories,
                p.AverageRating, p.CommentsCount, p.CreatedAt, p.CreatorName
            )).ToList(),
            stats.TopRatedProblems.Select(p => new ProblemSummaryDto(
                p.Id, p.Title, p.Description, p.Status, p.Priority,
                p.Latitude, p.Longitude, p.Categories,
                p.AverageRating, p.CommentsCount, p.CreatedAt, p.CreatorName
            )).ToList()
        );
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost("admin/message")]
    public async Task<ActionResult<AdminChatMessageResponse>> SendAdminMessage(
        [FromBody] AdminChatMessageRequest request,
        CancellationToken cancellationToken)
    {
        var response = await geminiService.ProcessAdminChatMessageAsync(request.Message, cancellationToken);

        return new AdminChatMessageResponse(
            response.Message,
            response.ResponseType.ToString(),
            response.SuggestedFilter != null
                ? new DashboardFilterDto(
                    response.SuggestedFilter.Status,
                    response.SuggestedFilter.Category,
                    response.SuggestedFilter.Priority,
                    response.SuggestedFilter.DateRange,
                    response.SuggestedFilter.SortBy)
                : null,
            response.Problems?.Select(p => new ProblemSummaryDto(
                p.Id, p.Title, p.Description, p.Status, p.Priority,
                p.Latitude, p.Longitude, p.Categories,
                p.AverageRating, p.CommentsCount, p.CreatedAt, p.CreatorName
            )).ToList()
        );
    }
}
