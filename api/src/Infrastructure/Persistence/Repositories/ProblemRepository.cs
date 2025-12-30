using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Identity.Users;
using Domain.Problems;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class ProblemRepository(ApplicationDbContext context) : IProblemQueries, IProblemRepository
{
    public async Task<(IReadOnlyList<Problem> Items, int TotalCount)> GetPaged(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var query = context.Problems
            .Include(x => x.Comments).ThenInclude(c => c.User)
            .Include(x => x.Images)
            .Include(x => x.CoordinatorImages)
            .Include(x => x.CreatedBy)
            .Include(x => x.Coordinator)
            .Include(x => x.Ratings)
            .AsSplitQuery()
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<IReadOnlyList<Problem>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Problems
            .Include(x => x.Comments)
            .Include(x => x.Images)
            .Include(x => x.CoordinatorImages)
            .Include(x => x.CreatedBy)
            .Include(x => x.Coordinator)
            .Include(x => x.Ratings)
            .AsSplitQuery()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Problem>> GetById(ProblemId id, CancellationToken cancellationToken)
    {
        var entity = await context.Problems
            .Include(x => x.Comments).ThenInclude(c => c.User)
            .Include(x => x.Images)
            .Include(x => x.CoordinatorImages)
            .Include(x => x.CreatedBy)
            .Include(x => x.Coordinator)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Problem>() : Option.Some(entity);
    }

    public async Task<Option<Problem>> SearchByTitle(string title, CancellationToken cancellationToken)
    {
        var entity = await context.Problems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Title == title, cancellationToken);

        return entity == null ? Option.None<Problem>() : Option.Some(entity);
    }

    public async Task<Problem> Add(Problem problem, CancellationToken cancellationToken)
    {
        await context.Problems.AddAsync(problem, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return problem;
    }

    public async Task<Problem> Update(Problem problem, CancellationToken cancellationToken)
    {
        context.Problems.Update(problem);
        await context.SaveChangesAsync(cancellationToken);
        return problem;
    }

    public async Task<Problem> Delete(Problem problem, CancellationToken cancellationToken)
    {
        context.Problems.Remove(problem);
        await context.SaveChangesAsync(cancellationToken);
        return problem;
    }

    public async Task<IReadOnlyList<Problem>> GetByUserId(UserId userId, CancellationToken cancellationToken)
    {
        return await context.Problems
            .Include(x => x.Comments).ThenInclude(c => c.User)
            .Include(x => x.Images)
            .Include(x => x.CoordinatorImages)
            .Include(x => x.CreatedBy)
            .Include(x => x.Coordinator)
            .Include(x => x.Ratings)
            .AsSplitQuery()
            .AsNoTracking()
            .Where(x => x.CreatedById == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Problem>> GetByCreatedBy(UserId userId, CancellationToken cancellationToken)
    {
        return await context.Problems
            .Include(x => x.Comments).ThenInclude(c => c.User)
            .Include(x => x.Images)
            .Include(x => x.CoordinatorImages)
            .Where(x => x.CreatedById == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Problem>> GetByCoordinatorId(UserId coordinatorId, CancellationToken cancellationToken)
    {
        return await context.Problems
            .Include(x => x.Comments).ThenInclude(c => c.User)
            .Include(x => x.Images)
            .Include(x => x.CoordinatorImages)
            .Include(x => x.CreatedBy)
            .Include(x => x.Coordinator)
            .Include(x => x.Ratings)
            .AsSplitQuery()
            .AsNoTracking()
            .Where(x => x.CoordinatorId == coordinatorId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Problem>> GetByStatus(ProblemStatus status, CancellationToken cancellationToken)
    {
        return await context.Problems
            .Include(x => x.Comments).ThenInclude(c => c.User)
            .Include(x => x.Images)
            .Include(x => x.CoordinatorImages)
            .Include(x => x.CreatedBy)
            .Include(x => x.Coordinator)
            .Include(x => x.Ratings)
            .AsSplitQuery()
            .AsNoTracking()
            .Where(x => x.Status == status)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Problem>> GetForMap(CancellationToken cancellationToken)
    {
        return await context.Problems
            .Include(x => x.Comments)
            .Include(x => x.Images)
            .Include(x => x.CoordinatorImages)
            .Include(x => x.CreatedBy)
            .Include(x => x.Coordinator)
            .Include(x => x.Ratings)
            .AsSplitQuery()
            .AsNoTracking()
            .Where(x => x.Status != ProblemStatus.New
                     && x.Status != ProblemStatus.Rejected)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Problem>> GetByUserIdFiltered(
        UserId userId,
        string? searchTerm,
        string? status,
        string? category,
        string? priority,
        string? sortBy,
        bool sortDescending,
        string? dateFilter,
        CancellationToken cancellationToken)
    {
        var query = context.Problems
            .Include(x => x.Comments).ThenInclude(c => c.User)
            .Include(x => x.Images)
            .Include(x => x.CoordinatorImages)
            .Include(x => x.CreatedBy)
            .Include(x => x.Coordinator)
            .Include(x => x.Ratings)
            .AsSplitQuery()
            .AsNoTracking()
            .Where(x => x.CreatedById == userId);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var lowerSearchTerm = searchTerm.ToLower();
            query = query.Where(x => x.Title.ToLower().Contains(lowerSearchTerm) 
                                  || x.Description.ToLower().Contains(lowerSearchTerm));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            var problemStatus = ProblemStatus.From(status);
            query = query.Where(x => x.Status == problemStatus);
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            var categoryValue = Category.From(category).Value;
            query = query.Where(x =>
                EF.Functions.JsonContains(
                    EF.Property<string>(x, nameof(Problem.Categories)),
                    $"[\"{categoryValue}\"]"));
        }

        if (!string.IsNullOrWhiteSpace(priority))
        {
            var problemPriority = Priority.From(priority);
            query = query.Where(x => x.Priority == problemPriority);
        }

        if (!string.IsNullOrWhiteSpace(dateFilter))
        {
            var now = DateTime.UtcNow;
            query = dateFilter.ToLower() switch
            {
                "week" => query.Where(x => x.CreatedAt >= now.AddDays(-7)),
                "month" => query.Where(x => x.CreatedAt >= now.AddMonths(-1)),
                "year" => query.Where(x => x.CreatedAt >= now.AddYears(-1)),
                _ => query
            };
        }

        query = sortBy?.ToLower() switch
        {
            "title" => sortDescending ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title),
            "priority" => sortDescending ? query.OrderByDescending(x => x.Priority.Value) : query.OrderBy(x => x.Priority.Value),
            "updatedat" => sortDescending ? query.OrderByDescending(x => x.UpdatedAt) : query.OrderBy(x => x.UpdatedAt),
            _ => sortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt)
        };

        return await query.ToListAsync(cancellationToken);
    }
}