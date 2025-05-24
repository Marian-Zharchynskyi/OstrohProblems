using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Statuses;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class StatusRepository(ApplicationDbContext context) : IStatusQueries , IStatusRepository
{
    public async Task<IReadOnlyList<Status>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Statuses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Status>> GetById(StatusId id, CancellationToken cancellationToken)
    {
        var entity = await context.Statuses
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Status>() : Option.Some(entity);
    }

    public async Task<Option<Status>> SearchByName(string name, CancellationToken cancellationToken)
    {
        var entity = await context.Statuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity == null ? Option.None<Status>() : Option.Some(entity);
    }

    public async Task<Status> Add(Status manufacturer, CancellationToken cancellationToken)
    {
        await context.Statuses.AddAsync(manufacturer, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return manufacturer;
    }

    public async Task<Status> Update(Status manufacturer, CancellationToken cancellationToken)
    {
        context.Statuses.Update(manufacturer);
        await context.SaveChangesAsync(cancellationToken);
        return manufacturer;
    }

    public async Task<Status> Delete(Status manufacturer, CancellationToken cancellationToken)
    {
        context.Statuses.Remove(manufacturer);
        await context.SaveChangesAsync(cancellationToken);
        return manufacturer;
    }
}