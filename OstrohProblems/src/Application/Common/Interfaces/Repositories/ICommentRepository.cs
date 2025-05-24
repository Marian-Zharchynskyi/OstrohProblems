using Domain.Comments;
using Optional;

namespace Application.Common.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<Option<Comment>> GetById(CommentId id, CancellationToken cancellationToken);
    Task<Comment> Add(Comment comment, CancellationToken cancellationToken);
    Task<Comment> Update(Comment comment, CancellationToken cancellationToken);
    Task<Comment> Delete(Comment comment, CancellationToken cancellationToken);
}