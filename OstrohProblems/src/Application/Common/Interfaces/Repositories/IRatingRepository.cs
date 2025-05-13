using Domain.Ratings;

namespace Application.Common.Interfaces.Repositories;

public interface IRatingRepository
{
    Task<Rating> Add(Rating rating, CancellationToken cancellationToken);
    Task<Rating> Update(Rating rating, CancellationToken cancellationToken);
    Task<Rating> Delete(Rating rating, CancellationToken cancellationToken);
}
