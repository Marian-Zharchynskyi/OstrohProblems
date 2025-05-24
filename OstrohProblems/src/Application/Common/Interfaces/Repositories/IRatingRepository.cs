using Domain.Ratings;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IRatingRepository
{
    Task<Option<Rating>> GetById(RatingId id, CancellationToken cancellationToken);
    Task<Rating> Add(Rating rating, CancellationToken cancellationToken);
    Task<Rating> Update(Rating rating, CancellationToken cancellationToken);
    Task<Rating> Delete(Rating rating, CancellationToken cancellationToken);
}
