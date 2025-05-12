using Domain.Problems;

namespace Application.Problems.Exceptions;

public abstract class ProblemException(ProblemId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public ProblemId ProblemId { get; } = id;
}

public class ProblemNotFoundException(ProblemId id) 
    : ProblemException(id, $"Problem with ID {id} not found");

public class ProblemAlreadyExistsException(ProblemId id) 
    : ProblemException(id, $"Problem already exists with ID {id}");

public class ProblemUnknownException(ProblemId id, Exception innerException)
    : ProblemException(id, $"Unknown exception for the Problem with ID {id}", innerException);

public class ProblemHasRelatedEntitiesException(ProblemId id)
    : ProblemException(id, $"Problem with ID {id} cannot be deleted because it has related entities.");
    
public class ImageSaveException(ProblemId id)
    : ProblemException(id, $"Problem under id: {id} have problems with saving images!");

public class ImageNotFoundException(ProblemImageId id)
    : ProblemException(ProblemId.Empty, $"Product image under id: {id} not found!");

public class ProblemConcurrencyException(ProblemId id)
    : ProblemException(id, $"Problem with ID {id.Value} was changed or deleted by another user.");
    
public class UserIdNotFoundException(ProblemId id)
    : ProblemException(id, "User ID not found in token");