using Domain.Identity.Users;
using Domain.Problems;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IProblemQueries
{
    Task<IReadOnlyList<Problem>> GetAll(CancellationToken cancellationToken);
    Task<Option<Problem>> GetById(ProblemId id, CancellationToken cancellationToken);
    Task<(IReadOnlyList<Problem> Items, int TotalCount)> GetPaged(int page, int pageSize,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// Get problems created by a specific user
    /// </summary>
    Task<IReadOnlyList<Problem>> GetByUserId(UserId userId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get problems assigned to a specific coordinator
    /// </summary>
    Task<IReadOnlyList<Problem>> GetByCoordinatorId(UserId coordinatorId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get problems by status
    /// </summary>
    Task<IReadOnlyList<Problem>> GetByStatus(ProblemStatus status, CancellationToken cancellationToken);
}