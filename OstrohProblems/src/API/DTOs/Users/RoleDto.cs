using Domain.Identity.Roles;

namespace API.DTOs.Users;

public record RoleDto(string Name)
{
    public static RoleDto FromDomainModel(Role role)
        => new(role.Name);
}