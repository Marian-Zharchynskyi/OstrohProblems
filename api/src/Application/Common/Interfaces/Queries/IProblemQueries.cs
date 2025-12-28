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
    
    /// <summary>
    /// Get problems for map display (excludes New, Completed, Rejected statuses)
    /// </summary>
    Task<IReadOnlyList<Problem>> GetForMap(CancellationToken cancellationToken);
    
    /// <summary>
    /// Get filtered problems by user with search, category, priority and sorting
    /// </summary>
    Task<IReadOnlyList<Problem>> GetByUserIdFiltered(
        UserId userId,
        string? searchTerm,
        string? status,
        string? category,
        string? priority,
        string? sortBy,
        bool sortDescending,
        string? dateFilter,
        CancellationToken cancellationToken);
}