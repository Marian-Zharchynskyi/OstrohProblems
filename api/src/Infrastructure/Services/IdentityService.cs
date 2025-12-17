using System.Security.Claims;
using Application.Common.Interfaces;
using Domain.Identity.Users;
using LanguageExt;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly HttpContext? _httpContext;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }

    public Option<UserId> GetUserId()
    {
        var userId = _httpContext?.User.FindFirstValue("id");
        return string.IsNullOrEmpty(userId)
            ? Option<UserId>.None
            : new UserId(new Guid(userId));
    }

    public Option<string> GetUserEmail()
    {
        var email = _httpContext?.User.FindFirstValue("email");
        return string.IsNullOrEmpty(email)
            ? Option<string>.None
            : Option<string>.Some(email);
    }

    public Option<string> GetUserName()
    {
        var name = _httpContext?.User.FindFirstValue("name");
        return string.IsNullOrEmpty(name) || name == "N/A"
            ? Option<string>.None
            : Option<string>.Some(name);
    }

    public Option<string[]> GetUserRoles()
    {
        var roles = _httpContext?.User.FindAll("role")
            .Select(c => c.Value)
            .ToArray();
        
        return roles == null || roles.Length == 0
            ? Option<string[]>.None
            : Option<string[]>.Some(roles);
    }
}
