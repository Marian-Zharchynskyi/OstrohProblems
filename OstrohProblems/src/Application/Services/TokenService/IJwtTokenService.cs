using System.Security.Claims;
using Domain.Identity.Users;
using Domain.ViewModels;

namespace Application.Services.TokenService
{
    public interface IJwtTokenService
    {
        Task<JwtVm> GenerateTokensAsync(User user, CancellationToken cancellationToken);
        ClaimsPrincipal GetPrincipals(string accessToken);
    }
}
