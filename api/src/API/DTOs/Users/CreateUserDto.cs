namespace API.DTOs.Users;

public record CreateUserDto(
    string Email,
    string Password,
    string? FullName,
    Guid RoleId);
