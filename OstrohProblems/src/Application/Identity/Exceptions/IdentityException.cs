
using Domain.Identity.Users;

namespace Application.Identity.Exceptions;

public abstract class IdentityException(UserId id, string message, Exception? innerException = null)
    : Exception(message, innerException)
{
    public UserId UserId { get; } = id;
}

public class UserByThisEmailAlreadyExistsException(UserId id) : IdentityException(id, $"User by this email already exists! User id: {id}");
public class EmailOrPasswordAreIncorrectException() : IdentityException(UserId.Empty, "Email or Password are incorrect!");
public class InvalidTokenException() : IdentityException(UserId.Empty, "Invalid token!");
public class TokenExpiredException() : IdentityException(UserId.Empty, "Token has expired!");
public class InvalidAccessTokenException() : IdentityException(UserId.Empty, "Token has expired!");
public class UserNorFoundException(UserId id) : IdentityException(id, $"User under id: {id} was not found!");

public class IdentityUnknownException(UserId id, Exception innerException)
    : IdentityException(id, $"Unknown exception for the user under id: {id}", innerException);