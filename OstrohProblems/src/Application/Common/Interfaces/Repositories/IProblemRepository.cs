using Domain.Problems;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface IProblemRepository
{
    Task<Option<Problem>> GetById(ProblemId id, CancellationToken cancellationToken);
    Task<Option<Problem>> SearchByTitle(string title, CancellationToken cancellationToken);
    Task<Problem> Add(Problem problem, CancellationToken cancellationToken);
    Task<Problem> Update(Problem problem, CancellationToken cancellationToken);
    Task<Problem> Delete(Problem problem, CancellationToken cancellationToken);
}