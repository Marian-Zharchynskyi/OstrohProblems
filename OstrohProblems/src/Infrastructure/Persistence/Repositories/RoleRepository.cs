using System.Linq.Expressions;
using Application.Common.Interfaces.Queries;
using Domain.Identity.Roles;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class RoleRepository(ApplicationDbContext context) : IRoleQueries
{
    public async Task<IReadOnlyList<Role>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Roles
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Role>> GetByName(string name, CancellationToken cancellationToken)
    {
        var entity = await GetRoleAsync(r => r.Name == name, cancellationToken, false);

        return entity == null ? Option.None<Role>() : Option.Some(entity);
    }

    public async Task<Option<Role>> GetById(RoleId id, CancellationToken cancellationToken)
    {
        var role = await context.Roles
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        return role == null ? Option.None<Role>() : Option.Some(role);
    }

    public async Task<Role?> GetRoleAsync(Expression<Func<Role, bool>> predicate, CancellationToken cancellationToken,
        bool asNoTracking = true)
    {
        if (asNoTracking)
        {
            return await context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        return await context.Roles
            .FirstOrDefaultAsync(predicate, cancellationToken);
    }
}