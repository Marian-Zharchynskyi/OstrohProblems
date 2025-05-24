using Domain.Comments;
using Domain.Problems;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface ICommentQueries
{
    Task<IReadOnlyList<Comment>> GetAll(CancellationToken cancellationToken);
    Task<Option<Comment>> GetById(CommentId id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Comment>> GetAllByProblemId(ProblemId problemId, CancellationToken cancellationToken);
    Task<(IReadOnlyList<Comment> Items, int TotalCount)> GetPaged(int page, int pageSize,
        CancellationToken cancellationToken);
}