using Application.Common;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Users.Exceptions;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using MediatR;

namespace Application.Users.Commands;

public record ChangeRolesForUserCommand : IRequest<Result<User, UserException>>
{
    public required Guid UserId { get; init; }
    public required List<Guid> RoleIds { get; init; }
}

public class ChangeRolesForUserCommandHandler(
    IUserRepository userRepository,
    IRoleQueries roleQueries)
    : IRequestHandler<ChangeRolesForUserCommand, Result<User, UserException>>
{
    public async Task<Result<User, UserException>> Handle(
        ChangeRolesForUserCommand request,
        CancellationToken cancellationToken)
    {
        var userId = new UserId(request.UserId);
        var existingUser = await userRepository.GetById(userId, cancellationToken);

        var rolesList = new List<Role>();

        foreach (var roleId in request.RoleIds)
        {
            var existingRole = await roleQueries.GetById(roleId, cancellationToken);

            var roleResult = await existingRole.Match<Task<Result<Role, UserException>>>(
                async r =>
                {
                    rolesList.Add(r);
                    return r;
                },
                () => Task.FromResult<Result<Role, UserException>>(new RoleNotFoundException(roleId))
            );

            if (roleResult.IsError)
            {
                return new RoleNotFoundException(roleId);
            }
        }

        return await existingUser.Match<Task<Result<User, UserException>>>(
            async user => await ChangeRolesForUser(user, rolesList, cancellationToken),
            () => Task.FromResult<Result<User, UserException>>(new UserNotFoundException(userId))
        );
    }

    private async Task<Result<User, UserException>> ChangeRolesForUser(
        User user,
        List<Role> roles,
        CancellationToken cancellationToken)
    {
        try
        {
            user.SetRoles(roles);
            return await userRepository.Update(user, cancellationToken);
        }
        catch (Exception exception)
        {
            return new UserUnknownException(user.Id, exception);
        }
    }
}
