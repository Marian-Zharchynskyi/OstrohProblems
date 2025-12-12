using Domain.Identity.Users;

namespace API.DTOs.Users;

public record UserDto(
    Guid? Id,
    string Email,
    string? FullName,
    UserImageDto? Image,
    List<RoleDto>? Roles)
{
    public static UserDto FromDomainModel(User user, Func<string, string>? getImageUrl = null)
        => new(
            user.Id.Value,
            user.Email,
            user.FullName,
            user.UserImage != null && getImageUrl != null 
                ? UserImageDto.FromDomainModel(user.UserImage, getImageUrl) 
                : null,
            user.Roles.Count == 0 ? null : user.Roles.Select(RoleDto.FromDomainModel).ToList());
}