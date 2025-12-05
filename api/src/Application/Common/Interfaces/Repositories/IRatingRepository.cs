using Domain.Identity.Users;
using Domain.Problems;
using Domain.Ratings;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IRatingRepository
{
    Task<Option<Rating>> GetById(RatingId id, CancellationToken cancellationToken);
    Task<Option<Rating>> GetByUserAndProblem(UserId userId, ProblemId problemId, CancellationToken cancellationToken);
    Task<Rating> Add(Rating rating, CancellationToken cancellationToken);
    Task<Rating> Update(Rating rating, CancellationToken cancellationToken);
    Task<Rating> Delete(Rating rating, CancellationToken cancellationToken);
}
