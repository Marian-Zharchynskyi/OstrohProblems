using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Ratings;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class RatingRepository(ApplicationDbContext context) : IRatingQueries, IRatingRepository
{
    public async Task<IReadOnlyList<Rating>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Ratings
            .Include(x => x.User)
            .Include(x => x.Problem)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Rating>> GetById(RatingId id, CancellationToken cancellationToken)
    {
        var entity = await context.Ratings
            .Include(x => x.User)
            .Include(x => x.Problem)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Rating>() : Option.Some(entity);
    }

    public async Task<Rating> Add(Rating rating, CancellationToken cancellationToken)
    {
        await context.Ratings.AddAsync(rating, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return rating;
    }

    public async Task<Rating> Update(Rating rating, CancellationToken cancellationToken)
    {
        context.Ratings.Update(rating);
        await context.SaveChangesAsync(cancellationToken);
        return rating;
    }

    public async Task<Rating> Delete(Rating rating, CancellationToken cancellationToken)
    {
        context.Ratings.Remove(rating);
        await context.SaveChangesAsync(cancellationToken);
        return rating;
    }
}