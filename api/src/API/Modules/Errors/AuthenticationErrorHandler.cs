using Application.Identity.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Modules.Errors;

public static class AuthenticationErrorHandler
{
    public static ObjectResult ToObjectResult(this IdentityException exception)
    {
        return new ObjectResult(exception.Message)
        {
            StatusCode = exception switch
            {
                UserByThisEmailAlreadyExistsException => StatusCodes.Status409Conflict,
                EmailOrPasswordAreIncorrectException => StatusCodes.Status401Unauthorized,
                IdentityUnknownException => StatusCodes.Status500InternalServerError,
                UserNorFoundException => StatusCodes.Status404NotFound,

                InvalidTokenException
                    or TokenExpiredException
                    or InvalidAccessTokenException
                    => StatusCodes.Status426UpgradeRequired,

                _ => throw new NotImplementedException("Authentication error handler does not implemented")
            }
        };
    }
}