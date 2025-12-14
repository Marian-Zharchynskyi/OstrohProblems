using Domain.Comments;
using Domain.Identity.Roles;
using Domain.Problems;
using Domain.Ratings;
using Domain.RefreshTokens;

namespace Domain.Identity.Users;

public class User
{
    public UserId Id { get; }
    public string Email { get; private set; }
    public string? FullName { get; private set; }
    public string PasswordHash { get; }
    public UserImage? UserImage { get; private set; }
    public ICollection<Problem> Problems { get; private set; } = new List<Problem>();
    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
    public ICollection<Rating> Ratings { get; private set; } = new List<Rating>();
    public RoleId RoleId { get; private set; }
    public Role? Role { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

    private User(UserId id, string email, string? fullName, string passwordHash, RoleId roleId)
    {
        Id = id;
        Email = email;
        FullName = fullName;
        PasswordHash = passwordHash;
        RoleId = roleId;
    }

    public static User New(UserId id, string email, string? fullName, string passwordHash, RoleId roleId)
        => new(id, email, fullName, passwordHash, roleId);

    public void UpdateUser(string email, string? fullName)
    {
        Email = email;
        FullName = fullName;
    }

    public void UpdateUserImage(UserImage userImage)
        => UserImage = userImage;

    public void RemoveUserImage()
        => UserImage = null;

    public void SetRole(RoleId roleId)
        => RoleId = roleId;
}