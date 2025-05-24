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
    public ICollection<Role> Roles { get; private set; } = new List<Role>();
    public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

    private User(UserId id, string email, string? fullName, string passwordHash)
    {
        Id = id;
        Email = email;
        FullName = fullName;
        PasswordHash = passwordHash;
    }

    public static User New(UserId id, string email, string? fullName, string passwordHash)
        => new(id, email, fullName, passwordHash);

    public void UpdateUser(string email, string? fullName)
    {
        Email = email;
        FullName = fullName;
    }

    public void UpdateUserImage(UserImage userImage)
        => UserImage = userImage;

    public void SetRoles(List<Role> roles)
        => Roles = roles;
}