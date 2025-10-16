using Application.Categories.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.Errors;

public static class CategoryErrorHandler
{
    public static ObjectResult ToObjectResult(this CategoryException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                CategoryNotFoundException => StatusCodes.Status404NotFound,
                CategoryAlreadyExistsException => StatusCodes.Status409Conflict,
                CategoryUnknownException => StatusCodes.Status500InternalServerError,
                _ => throw new NotImplementedException("Problem category error handler does not implemented")
            }
        };
    }
}