using Domain.Comments;

namespace Application.Comments.Exceptions;

public abstract class CommentException(CommentId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public CommentId CommentId { get; } = id;
}

public class CommentNotFoundException(CommentId id)
    : CommentException(id, $"Comment with ID {id} not found");

public class CommentAlreadyExistsException(CommentId id)
    : CommentException(id, $"Comment already exists with ID {id}");

public class CommentUnknownException(CommentId id, Exception innerException)
    : CommentException(id, $"Unknown exception for the Comment with ID {id}", innerException);

public class UserIdNotFoundException(CommentId id)
    : CommentException(id, "User ID not found in token");

public class CommentHasRelatedEntitiesException(CommentId id)
    : CommentException(id, $"Comment with ID {id} cannot be deleted because it has related entities.");