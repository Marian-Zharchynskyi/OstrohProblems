using Application.Common;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Services.HashPasswordService;
using Application.Users.Exceptions;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using MediatR;

namespace Application.Users.Commands;

public record CreateUserCommand : IRequest<Result<User, UserException>>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public required Guid RoleId { get; init; }
}

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IRoleQueries roleQueries,
    IHashPasswordService hashPasswordService)
    : IRequestHandler<CreateUserCommand, Result<User, UserException>>
{
    public async Task<Result<User, UserException>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await userRepository.SearchByEmail(request.Email, cancellationToken);

        return await existingUser.Match(
            _ => Task.FromResult<Result<User, UserException>>(
                new UserByThisEmailAlreadyExistsException(UserId.Empty)),
            async () => await CreateUser(request, cancellationToken));
    }

    private async Task<Result<User, UserException>> CreateUser(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var roleId = new RoleId(request.RoleId);
            var existingRole = await roleQueries.GetById(roleId, cancellationToken);

            return await existingRole.Match<Task<Result<User, UserException>>>(
                async role =>
                {
                    var passwordHash = hashPasswordService.HashPassword(request.Password);
                    var userId = UserId.New();
                    var user = User.New(userId, request.Email, request.Name, request.Surname, null, passwordHash, role.Id);

                    var createdUser = await userRepository.Create(user, cancellationToken);
                    return createdUser;
                },
                () => Task.FromResult<Result<User, UserException>>(new RoleNotFoundException(roleId.Value))
            );
        }
        catch (Exception exception)
        {
            return new UserUnknownException(UserId.Empty, exception);
        }
    }
}
