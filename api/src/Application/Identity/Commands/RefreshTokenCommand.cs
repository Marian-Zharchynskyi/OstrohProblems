using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Identity.Exceptions;
using Application.Services.TokenService;
using Domain.RefreshTokens;
using Domain.ViewModels;
using MediatR;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Application.Identity.Commands;

public record RefreshTokenCommand : IRequest<Result<JwtVm, IdentityException>>
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}

public class RefreshTokenCommandHandler(
    IJwtTokenService jwtTokenService,
    IRefreshTokenRepository refreshTokenRepository,
    IUserRepository userRepository
)
    : IRequestHandler<RefreshTokenCommand, Result<JwtVm, IdentityException>>
{
    public async Task<Result<JwtVm, IdentityException>> Handle(RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var existingRefreshToken =
            await refreshTokenRepository.GetRefreshTokenAsync(request.RefreshToken, cancellationToken);

        return await existingRefreshToken.Match(
            async rt => await RefreshToken(rt, request.AccessToken, cancellationToken),
            () => Task.FromResult<Result<JwtVm, IdentityException>>(
                new InvalidTokenException()));
    }

    private async Task<Result<JwtVm, IdentityException>> RefreshToken(RefreshToken storedToken,
        string accessToken, CancellationToken cancellationToken)
    {
        if (storedToken.IsUsed)
        {
            return await Task.FromResult<Result<JwtVm, IdentityException>>(new InvalidTokenException());
        }

        if (storedToken.ExpiredDate < DateTime.UtcNow)
        {
            return await Task.FromResult<Result<JwtVm, IdentityException>>(new TokenExpiredException());
        }

        var principals = jwtTokenService.GetPrincipals(accessToken);

        var accessTokenId = principals.Claims
            .Single(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

        if (storedToken.JwtId != accessTokenId)
        {
            return await Task.FromResult<Result<JwtVm, IdentityException>>(new InvalidAccessTokenException());
        }

        var existingUser = await userRepository.GetById(storedToken.UserId, cancellationToken);

        return await existingUser.Match<Task<Result<JwtVm, IdentityException>>>(
            async u =>
            {
                await refreshTokenRepository.MarkAsUsed(storedToken.Id, cancellationToken);
                return await jwtTokenService.GenerateTokensAsync(u, cancellationToken);
            },
            () => Task.FromResult<Result<JwtVm, IdentityException>>(
                new UserNorFoundException(storedToken.UserId)));
    }
}