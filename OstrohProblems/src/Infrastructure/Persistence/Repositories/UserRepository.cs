using System.Linq.Expressions;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository, IUserQueries
{
    public async Task<User> Create(User user, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User> Update(User user, CancellationToken cancellationToken)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<User> Delete(User user, CancellationToken cancellationToken)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<IReadOnlyList<User>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Users
            .Include(x => x.Roles)
            .Include(u => u.UserImage)
            .AsNoTracking()
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Option<User>> GetById(UserId id, CancellationToken cancellationToken)
    {
        var entity = await GetUserAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<User>() : Option.Some(entity);
    }
    
    public async Task<Option<User>> SearchByEmail(string email, CancellationToken cancellationToken)
    {
        var entity = await GetUserAsync(x => x.Email == email, cancellationToken);

        return entity == null ? Option.None<User>() : Option.Some(entity);
    }

    public async Task<Option<User>> SearchByEmailForUpdate(UserId userId, string email,
        CancellationToken cancellationToken)
    {
        var entity = await GetUserAsync(x => x.Email == email && x.Id != userId, cancellationToken);

        return entity == null ? Option.None<User>() : Option.Some(entity);
    }

    public async Task<User> AddRole(UserId userId, RoleId roleId, CancellationToken cancellationToken)
    {
        var entity = await GetUserAsync(x => x.Id == userId, cancellationToken);

        var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);
        entity.Roles.Add(role);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken,
        bool asNoTracking = false)
    {
        if (asNoTracking)
        {
            return await context.Users
                .Include(u => u.Roles)
                .Include(u => u.UserImage)
                .Include(x => x.Problems)
                .Include(x => x.Ratings)
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        return await context.Users
            .Include(u => u.Roles)
            .Include(u => u.UserImage)
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }
}