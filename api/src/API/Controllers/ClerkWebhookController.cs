using API.Modules.Errors;
using Application.Identity.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace API.Controllers;

[Route("webhooks/clerk")]
[ApiController]
public class ClerkWebhookController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> HandleWebhook(CancellationToken cancellationToken)
    {
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync(cancellationToken);
        
        try
        {
            var webhookEvent = JsonSerializer.Deserialize<ClerkWebhookEvent>(body);
            
            if (webhookEvent == null || webhookEvent.Type == null)
            {
                return BadRequest("Invalid webhook payload");
            }

            switch (webhookEvent.Type)
            {
                case "user.created":
                case "user.updated":
                    await HandleUserSync(webhookEvent, cancellationToken);
                    break;
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error processing webhook: {ex.Message}");
        }
    }

    private async Task HandleUserSync(ClerkWebhookEvent webhookEvent, CancellationToken cancellationToken)
    {
        if (webhookEvent.Data == null) return;

        var data = webhookEvent.Data;
        var email = data.EmailAddresses?.FirstOrDefault()?.EmailAddress;
        
        if (string.IsNullOrEmpty(email)) return;

        var firstName = data.FirstName;
        var lastName = data.LastName;
        
        var publicMetadata = data.PublicMetadata;
        string? role = null;
        if (publicMetadata != null && publicMetadata.ContainsKey("role"))
        {
            role = publicMetadata["role"]?.ToString();
        }

        var command = new SyncClerkUserCommand
        {
            ClerkUserId = data.Id ?? string.Empty,
            Email = email,
            Name = firstName,
            Surname = lastName,
            Role = role
        };

        await sender.Send(command, cancellationToken);
    }
}

public class ClerkWebhookEvent
{
    public string? Type { get; set; }
    public ClerkUserData? Data { get; set; }
}

public class ClerkUserData
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<ClerkEmailAddress>? EmailAddresses { get; set; }
    public Dictionary<string, object>? PublicMetadata { get; set; }
}

public class ClerkEmailAddress
{
    public string? EmailAddress { get; set; }
}
