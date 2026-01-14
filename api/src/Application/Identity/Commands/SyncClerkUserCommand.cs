using Application.Common;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Identity.Exceptions;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using MediatR;
using Optional.Unsafe;

namespace Application.Identity.Commands;

public class SyncClerkUserCommand : IRequest<Result<User, IdentityException>>
{
    public required string ClerkUserId { get; init; }
    public required string Email { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string? Role { get; init; }
}

public class SyncClerkUserCommandHandler(
    IUserRepository userRepository,
    IRoleQueries roleQueries) 
    : IRequestHandler<SyncClerkUserCommand, Result<User, IdentityException>>
{
    public async Task<Result<User, IdentityException>> Handle(
        SyncClerkUserCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.SearchByEmail(request.Email, cancellationToken);

        return await existingUser.Match(
            async u => await Task.FromResult<Result<User, IdentityException>>(u),
            async () => await CreateUserFromClerk(request, cancellationToken));
    }

    private async Task<Result<User, IdentityException>> CreateUserFromClerk(
        SyncClerkUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var roleName = request.Role ?? RoleNames.User;
            var roleResult = await roleQueries.GetByName(roleName, cancellationToken);

            if (!roleResult.HasValue)
            {
                roleResult = await roleQueries.GetByName(RoleNames.User, cancellationToken);
                if (!roleResult.HasValue)
                {
                    return new IdentityUnknownException(UserId.Empty, 
                        new Exception("Default role not found"));
                }
            }

            var role = roleResult.ValueOrFailure();
            var userId = UserId.New();
            var user = User.New(
                userId, 
                request.Email, 
                request.Name, 
                request.Surname, 
                null, 
                string.Empty,
                role.Id,
                request.ClerkUserId);

            await userRepository.Create(user, cancellationToken);

            return user;
        }
        catch (Exception exception)
        {
            return new IdentityUnknownException(UserId.Empty, exception);
        }
    }
}
