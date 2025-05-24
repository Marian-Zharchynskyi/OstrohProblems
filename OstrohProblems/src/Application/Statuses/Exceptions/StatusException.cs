using Domain.Statuses;

namespace Application.Statuses.Exceptions;


public abstract class StatusException(StatusId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public StatusId ManufacturerId { get; } = id;
}

public class StatusNotFoundException(StatusId id) 
    : StatusException(id, $"ProblemStatus under id: {id} not found");

public class StatusAlreadyExistsException(StatusId id) 
    : StatusException(id, $"ProblemStatus already exists: {id}");

public class StatusUnknownException(StatusId id, Exception innerException)
    : StatusException(id, $"Unknown exception for the ProblemStatus under id: {id}", innerException);
    
public class StatusHasRelatedProductsException(StatusId id)
    : StatusException(id, $"ProblemStatus with ID {id} cannot be deleted because it has related products.");