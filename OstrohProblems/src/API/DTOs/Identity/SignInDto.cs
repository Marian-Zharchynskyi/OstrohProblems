using Domain.Identity.Users;

namespace API.DTOs.Identity;

public record SignInDto(string Email, string Password)
{
    public static SignInDto FromDomainModel(User user)
        => new(user.Email, user.PasswordHash);
}