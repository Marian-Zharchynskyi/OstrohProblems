using Domain.Comments;
using Domain.Identity.Users;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<Option<Comment>> GetById(CommentId id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Comment>> GetByCreatedBy(UserId userId, CancellationToken cancellationToken);
    Task<Comment> Add(Comment comment, CancellationToken cancellationToken);
    Task<Comment> Update(Comment comment, CancellationToken cancellationToken);
    Task<Comment> Delete(Comment comment, CancellationToken cancellationToken);
}