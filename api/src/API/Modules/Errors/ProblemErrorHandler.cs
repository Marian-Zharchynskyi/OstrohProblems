using Application.Problems.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.Errors;

public static class ProblemErrorHandler
{
    public static ObjectResult ToObjectResult(this ProblemException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                ProblemNotFoundException
                    or ImageNotFoundException 
                    or UserIdNotFoundException
                    => StatusCodes.Status404NotFound,
                
                ProblemAlreadyExistsException 
                    => StatusCodes.Status409Conflict,
                
                ProblemUnknownException 
                    or ImageSaveException 
                    or ProblemConcurrencyException
                    => StatusCodes.Status500InternalServerError,
                
                _ => throw new NotImplementedException("Problem error handler does not implemented")
            }
        };
    }
}