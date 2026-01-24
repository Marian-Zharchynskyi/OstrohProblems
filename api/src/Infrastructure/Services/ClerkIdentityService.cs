using System.Security.Claims;
using Application.Common.Interfaces;
using Domain.Identity.Users;
using LanguageExt;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class ClerkIdentityService : IIdentityService
{
    private readonly HttpContext? _httpContext;

    public ClerkIdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }

    public Option<UserId> GetUserId()
    {
        var clerkUserId = _httpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (string.IsNullOrEmpty(clerkUserId))
        {
            clerkUserId = _httpContext?.User.FindFirstValue("sub");
        }
        
        if (string.IsNullOrEmpty(clerkUserId))
        {
            return Option<UserId>.None;
        }

        var userIdClaim = _httpContext?.User.FindFirstValue("user_id");
        if (!string.IsNullOrEmpty(userIdClaim) && Guid.TryParse(userIdClaim, out var userId))
        {
            return new UserId(userId);
        }

        return Option<UserId>.None;
    }

    public Option<string> GetUserEmail()
    {
        var email = _httpContext?.User.FindFirstValue(ClaimTypes.Email);
        
        if (string.IsNullOrEmpty(email))
        {
            email = _httpContext?.User.FindFirstValue("email");
        }
        
        return string.IsNullOrEmpty(email)
            ? Option<string>.None
            : Option<string>.Some(email);
    }

    public Option<string> GetUserName()
    {
        var name = _httpContext?.User.FindFirstValue(ClaimTypes.Name);
        
        if (string.IsNullOrEmpty(name))
        {
            name = _httpContext?.User.FindFirstValue("name");
        }
        
        return string.IsNullOrEmpty(name) || name == "N/A"
            ? Option<string>.None
            : Option<string>.Some(name);
    }

    public Option<string[]> GetUserRoles()
    {
        var roles = _httpContext?.User.FindAll(ClaimTypes.Role)
            .Select(c => c.Value)
            .ToArray();

        if (roles == null || roles.Length == 0)
        {
            roles = _httpContext?.User.FindAll("role")
                .Select(c => c.Value)
                .ToArray();
        }
        
        if (roles == null || roles.Length == 0)
        {
            var metadata = _httpContext?.User.FindFirstValue("public_metadata");
            if (!string.IsNullOrEmpty(metadata))
            {
                try
                {
                    var metadataObj = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(metadata);
                    if (metadataObj != null && metadataObj.ContainsKey("role"))
                    {
                        var role = metadataObj["role"]?.ToString();
                        if (!string.IsNullOrEmpty(role))
                        {
                            roles = new[] { role };
                        }
                    }
                }
                catch
                {
                }
            }
        }
        
        return roles == null || roles.Length == 0
            ? Option<string[]>.None
            : Option<string[]>.Some(roles);
    }

    public bool IsUserInRole(string role)
    {
        return GetUserRoles()
            .Match(roles => roles.Contains(role), () => false);
    }
}
