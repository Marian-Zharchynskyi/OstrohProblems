using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Users.Exceptions;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using MediatR;

namespace Application.Users.Commands;

public record ChangeRoleForUserCommand : IRequest<Result<User, UserException>>
{
    public required Guid UserId { get; init; }
    public required Guid RoleId { get; init; }
}

public class ChangeRolesForUserCommandHandler(
    IUserRepository userRepository,
    IRoleQueries roleQueries,
    IClerkApiService clerkApiService)
    : IRequestHandler<ChangeRoleForUserCommand, Result<User, UserException>>
{
    public async Task<Result<User, UserException>> Handle(
        ChangeRoleForUserCommand request,
        CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var existingUser = await userRepository.GetById(userId, cancellationToken);

        var roleId = new RoleId(request.RoleId);
        var existingRole = await roleQueries.GetById(roleId, cancellationToken);

        return await existingRole.Match<Task<Result<User, UserException>>>(
            async role =>
            {
                return await existingUser.Match<Task<Result<User, UserException>>>(
                    async user => await ChangeRolesForUser(user, role, cancellationToken),
                    () => Task.FromResult<Result<User, UserException>>(new UserNotFoundException(userId))
                );
            },
            () => Task.FromResult<Result<User, UserException>>(new RoleNotFoundException(roleId.Value))
        );
    }

    private async Task<Result<User, UserException>> ChangeRolesForUser(
        User user,
        Role role,
        CancellationToken cancellationToken)
    {
        try
        {
            user.SetRole(role.Id);
            var updatedUser = await userRepository.Update(user, cancellationToken);
            
            // Sync role with Clerk if user has a ClerkId
            if (!string.IsNullOrEmpty(user.ClerkId))
            {
                await clerkApiService.UpdateUserMetadataAsync(user.ClerkId, new Dictionary<string, object>
                {
                    { "role", role.Name }
                });
            }
            
            return updatedUser;
        }
        catch (Exception exception)
        {
            return new UserUnknownException(user.Id, exception);
        }
    }
}
