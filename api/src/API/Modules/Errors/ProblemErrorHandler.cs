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
                    or CoordinatorImageNotFoundException
                    or UserIdNotFoundException
                    => StatusCodes.Status404NotFound,
                
                ProblemAlreadyExistsException
                    or ProblemWithTitleAlreadyExistsException
                    => StatusCodes.Status409Conflict,
                
                MaxImagesExceededException
                    or MaxCoordinatorImagesExceededException
                    => StatusCodes.Status400BadRequest,
                
                ProblemHasRelatedEntitiesException
                    => StatusCodes.Status409Conflict,
                
                ProblemUnknownException 
                    or ImageSaveException 
                    or ProblemConcurrencyException
                    => StatusCodes.Status500InternalServerError,
                
                _ => StatusCodes.Status500InternalServerError
            }
        };
    }
}