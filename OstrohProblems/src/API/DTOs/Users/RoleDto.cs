using Domain.Identity.Roles;

namespace API.DTOs.Users;

public record RoleDto(Guid? id, string Name)
{
    public static RoleDto FromDomainModel(Role role)
        => new(
            role.Id.Value,
            role.Name);
}