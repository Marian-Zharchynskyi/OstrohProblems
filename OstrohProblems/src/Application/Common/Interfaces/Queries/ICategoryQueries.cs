using Domain.Categories;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface ICategoryQueries
{
    Task<IReadOnlyList<Category>> GetAll(CancellationToken cancellationToken);
    Task<Option<Category>> GetById(CategoryId id, CancellationToken cancellationToken);
    Task<(IReadOnlyList<Category> Items, int TotalCount)> GetPaged(int page, int pageSize, CancellationToken cancellationToken);
}