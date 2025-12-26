namespace Domain.ViewModels;

public class UpdateUserVm
{
    public required string Email { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? PhoneNumber { get; set; }
}