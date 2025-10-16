using Application.Common;
using Application.Common.Interfaces.Repositories;
using Application.Identity.Exceptions;
using Application.Services.HashPasswordService;
using Application.Services.TokenService;
using Domain.Identity.Users;
using Domain.ViewModels;
using MediatR;

namespace Application.Identity.Commands;

public class SignInCommand: IRequest<Result<JwtVm, IdentityException>>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public class SignInCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService, IHashPasswordService hashPasswordService) 
    : IRequestHandler<SignInCommand, Result<JwtVm, IdentityException>>
{
    public async Task<Result<JwtVm, IdentityException>> Handle(
        SignInCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.SearchByEmail(request.Email, cancellationToken);
        
        return await existingUser.Match(
            async u => await SignIn(u, request.Password, cancellationToken),
            () => Task.FromResult<Result<JwtVm, IdentityException>>(new EmailOrPasswordAreIncorrectException()));
    }
    private async Task<Result<JwtVm, IdentityException>> SignIn(
         User user,
         string password,
         CancellationToken cancellationToken)
     {
         string storedHash = user.PasswordHash;

         if (!hashPasswordService.VerifyPassword(password, storedHash))
         {
             return new EmailOrPasswordAreIncorrectException();
         }

         try
         {
             var token = await jwtTokenService.GenerateTokensAsync(user, cancellationToken);
             return token;
         }
         catch (Exception exception)
         {
             return new IdentityUnknownException(user.Id, exception);
         }
     }
}