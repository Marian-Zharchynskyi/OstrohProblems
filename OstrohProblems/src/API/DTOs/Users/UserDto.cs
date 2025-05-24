using Domain.Identity.Users;

namespace API.DTOs.Users;

public record UserDto(
    Guid? Id,
    string Email,
    string? FullName,
    UserImageDto? Image,
    List<RoleDto>? Roles)
{
    public static UserDto FromDomainModel(User user)
        => new(
            user.Id.Value,
            user.Email,
            user.FullName,
            user.UserImage != null ? UserImageDto.FromDomainModel(user.UserImage) : null,
            user.Roles.Count == 0 ? null : user.Roles.Select(RoleDto.FromDomainModel).ToList());
}