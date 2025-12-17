namespace Domain.Identity.Roles;

public class Role
{
    public RoleId Id { get; }
    public string Name { get; set; }
    
    private Role(RoleId id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public static Role New(RoleId id, string name)
        => new(id, name);
}