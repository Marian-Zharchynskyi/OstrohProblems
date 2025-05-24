using Domain.Problems;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IProblemQueries
{
    Task<IReadOnlyList<Problem>> GetAll(CancellationToken cancellationToken);
    Task<Option<Problem>> GetById(ProblemId id, CancellationToken cancellationToken);
    Task<(IReadOnlyList<Problem> Items, int TotalCount)> GetPaged(int page, int pageSize,
        CancellationToken cancellationToken);
}