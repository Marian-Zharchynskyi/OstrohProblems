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
    public string? Name { get; private set; }
    public string? Surname { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string PasswordHash { get; private set; }
    public UserImage? UserImage { get; private set; }
    public ICollection<Problem> Problems { get; private set; } = new List<Problem>();
    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
    public ICollection<Rating> Ratings { get; private set; } = new List<Rating>();
    public RoleId RoleId { get; private set; }
    public Role? Role { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

    private User(UserId id, string email, string? name, string? surname, string? phoneNumber, string passwordHash, RoleId roleId)
    {
        Id = id;
        Email = email;
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;
        RoleId = roleId;
    }

    public static User New(UserId id, string email, string? name, string? surname, string? phoneNumber, string passwordHash, RoleId roleId)
        => new(id, email, name, surname, phoneNumber, passwordHash, roleId);

    public void UpdateUser(string email, string? name, string? surname, string? phoneNumber)
    {
        Email = email;
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
    }

    public void UpdatePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public void UpdateUserImage(UserImage userImage)
        => UserImage = userImage;

    public void RemoveUserImage()
        => UserImage = null;

    public void SetRole(RoleId roleId)
        => RoleId = roleId;
}