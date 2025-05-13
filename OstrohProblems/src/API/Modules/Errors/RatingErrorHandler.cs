using Application.Ratings.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.Errors;

public static class RatingErrorHandler
{
    public static ObjectResult ToObjectResult(this RatingException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                RatingNotFoundException => StatusCodes.Status404NotFound,
                RatingAlreadyExistsException => StatusCodes.Status409Conflict,
                RatingUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Rating error handler does not implemented")
            }
        };
    }
}