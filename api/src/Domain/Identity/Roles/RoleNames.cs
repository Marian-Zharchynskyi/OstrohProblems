namespace Domain.Identity.Roles;

public static class RoleNames
{
    public const string User = "User";
    public const string Admin = "Administrator";
    public const string Coordinator = "Coordinator";

    public static readonly IReadOnlyList<string> All = new List<string>
    {
        User,
        Admin,
        Coordinator
    };
    
    public static bool IsValid(string role) => All.Contains(role);
}