using Application.Statuses.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.Errors;

public static class StatusErrorHandler
{
    public static ObjectResult ToObjectResult(this StatusException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                StatusNotFoundException => StatusCodes.Status404NotFound,
                StatusAlreadyExistsException => StatusCodes.Status409Conflict,
                StatusUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Problem status error handler does not implemented")
            }
        };
    }
}