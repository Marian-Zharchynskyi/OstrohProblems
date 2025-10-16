using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class CategoryRepository(ApplicationDbContext context)
    : ICategoryRepository, ICategoryQueries
{
    public async Task<IReadOnlyList<Category>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Categories
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Option<Category>> GetById(CategoryId id, CancellationToken cancellationToken)
    {
        var entity = await context.Categories
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity == null ? Option.None<Category>() : Option.Some(entity);
    }

    public async Task<Option<Category>> SearchByName(string name, CancellationToken cancellationToken)
    {
        var entity = await context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity == null ? Option.None<Category>() : Option.Some(entity);
    }

    public async Task<Category> Add(Category category, CancellationToken cancellationToken)
    {
        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<Category> Update(Category category, CancellationToken cancellationToken)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<Category> Delete(Category category, CancellationToken cancellationToken)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<List<Category>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        var problemCategoryIds = ids.Select(id => new CategoryId(id)).ToList();

        return await context.Categories
            .Where(pc => problemCategoryIds.Contains(pc.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Category> Items, int TotalCount)> GetPaged(int page, int pageSize,
        CancellationToken cancellationToken)
    {
        var query = context.Categories.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }
}