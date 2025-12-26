namespace API.DTOs.Users;

public record ChangePasswordDto(
    string CurrentPassword,
    string NewPassword);
