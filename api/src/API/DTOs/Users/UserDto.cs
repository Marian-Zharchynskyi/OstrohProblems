using Domain.Identity.Users;

namespace API.DTOs.Users;

public record UserDto(
    Guid? Id,
    string Email,
    string? Name,
    string? Surname,
    string? PhoneNumber,
    UserImageDto? Image,
    RoleDto? Role)
{
    public static UserDto FromDomainModel(User user, Func<string, string>? getImageUrl = null)
        => new(
            user.Id.Value,
            user.Email,
            user.Name,
            user.Surname,
            user.PhoneNumber,
            user.UserImage != null && getImageUrl != null 
                ? UserImageDto.FromDomainModel(user.UserImage, getImageUrl) 
                : null,
            user.Role != null ? RoleDto.FromDomainModel(user.Role) : null);
}