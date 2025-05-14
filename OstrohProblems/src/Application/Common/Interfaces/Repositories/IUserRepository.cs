using Domain.Identity.Roles;
using Domain.Identity.Users;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> Create(User user, CancellationToken cancellationToken);
    Task<User> Update(User user, CancellationToken cancellationToken);
    Task<User> Delete(User user, CancellationToken cancellationToken);
    Task<User> AddRole(UserId userId, RoleId roleId, CancellationToken cancellationToken);
    Task<Option<User>> GetById(UserId id, CancellationToken cancellationToken);
    Task<Option<User>> SearchByEmail(string email, CancellationToken cancellationToken);
    Task<Option<User>> SearchByEmailForUpdate(UserId userId, string email, CancellationToken cancellationToken);
}