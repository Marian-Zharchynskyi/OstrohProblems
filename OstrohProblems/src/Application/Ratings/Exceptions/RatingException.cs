using Domain.Ratings;

namespace Application.Ratings.Exceptions;

public abstract class RatingException(RatingId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public RatingId RatingId { get; } = id;
}

public class RatingNotFoundException(RatingId id)
    : RatingException(id, $"Rating with ID {id} not found");

public class RatingAlreadyExistsException(RatingId id)
    : RatingException(id, $"Rating already exists with ID {id}");

public class RatingUnknownException(RatingId id, Exception innerException)
    : RatingException(id, $"Unknown exception for the Rating with ID {id}", innerException);

public class UserIdNotFoundException(RatingId id)
    : RatingException(id, "User ID not found in token");

public class RatingHasRelatedEntitiesException(RatingId id)
    : RatingException(id, $"Rating with ID {id} cannot be deleted because it has related entities.");