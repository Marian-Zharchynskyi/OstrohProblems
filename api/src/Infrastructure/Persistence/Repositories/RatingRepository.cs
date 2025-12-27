using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Identity.Users;
using Domain.Problems;
using Domain.Ratings;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class RatingRepository(ApplicationDbContext context) : IRatingQueries, IRatingRepository
{
    public async Task<(IReadOnlyList<Rating> Items, int TotalCount)> GetPaged(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var query = context.Ratings
            .Include(x => x.User)
            .Include(x => x.Problem)
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }
    
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

    public async Task<Option<Rating>> GetByUserAndProblem(UserId userId, ProblemId problemId, CancellationToken cancellationToken)
    {
        var entity = await context.Ratings
            .Include(x => x.User)
            .Include(x => x.Problem)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.ProblemId == problemId, cancellationToken);

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

    public async Task<IReadOnlyList<Rating>> GetByCreatedBy(UserId userId, CancellationToken cancellationToken)
    {
        return await context.Ratings
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
    }
}