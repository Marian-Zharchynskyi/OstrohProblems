using Application.Common;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Identity.Exceptions;
using Application.Services.HashPasswordService;
using Application.Services.TokenService;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Domain.ViewModels;
using MediatR;
using Optional.Unsafe;

namespace Application.Identity.Commands;

public class SignUpCommand : IRequest<Result<JwtVm, IdentityException>>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string? Name { get; init; }
}

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IJwtTokenService jwtTokenService,
    IHashPasswordService hashPasswordService,
    IRoleQueries roleQueries) 
    : IRequestHandler<SignUpCommand, Result<JwtVm, IdentityException>>
{
    public async Task<Result<JwtVm, IdentityException>> Handle(
        SignUpCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.SearchByEmail(request.Email, cancellationToken);

        return await existingUser.Match(
            u => Task.FromResult<Result<JwtVm, IdentityException>>(
                new UserByThisEmailAlreadyExistsException(u.Id)),
            async () => await SignUp(request.Email, request.Password, request.Name, cancellationToken));
    }

    private async Task<Result<JwtVm, IdentityException>> SignUp(
        string email,
        string password,
        string? name,
        CancellationToken cancellationToken)
    {
        try
        {
            var roleResult = await roleQueries.GetByName(RoleNames.User, cancellationToken);

            if (!roleResult.HasValue)
            {
                return new IdentityUnknownException(UserId.Empty, 
                    new Exception("Default role not found"));
            }

            var userId = UserId.New();
            var user = User.New(userId, email, name, hashPasswordService.HashPassword(password));

            await userRepository.Create(user, cancellationToken);

            var role = roleResult.ValueOrFailure();
            var userWithRole = await userRepository.AddRole(userId, role.Id, cancellationToken);


            var token = await jwtTokenService.GenerateTokensAsync(userWithRole, cancellationToken);

            return token;
        }
        catch (Exception exception)
        {
            return new IdentityUnknownException(UserId.Empty, exception);
        }
    }
}