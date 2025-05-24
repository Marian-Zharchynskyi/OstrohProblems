using Domain.Categories;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface ICategoryRepository
{
    Task<Option<Category>> GetById(CategoryId id, CancellationToken cancellationToken);
    Task<Option<Category>> SearchByName(string name, CancellationToken cancellationToken);
    Task<Category> Add(Category status, CancellationToken cancellationToken);
    Task<Category> Update(Category status, CancellationToken cancellationToken);
    Task<Category> Delete(Category status, CancellationToken cancellationToken);
    Task<List<Category>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
}