using Application.Services.HashPasswordService;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DataSeed
{
    public static void Seed(ModelBuilder modelBuilder, IHashPasswordService hashPasswordService)
    {
        var roles = _seedRoles(modelBuilder); // Тепер отримуємо список ролей
        _seedUsers(modelBuilder, hashPasswordService, roles); // Передаємо ролі в метод створення користувачів
    }

    private static List<Role> _seedRoles(ModelBuilder modelBuilder)
    {
        var roles = new List<Role>
        {
            Role.New(RoleId.New(), RoleNames.Admin),
            Role.New(RoleId.New(), RoleNames.User)
        };

        modelBuilder.Entity<Role>().HasData(roles);
        return roles; // Повертаємо ролі, щоб передати їх далі
    }

    private static void _seedUsers(ModelBuilder modelBuilder, IHashPasswordService hashPasswordService, List<Role> roles)
    {
        var admin = User.New(
            UserId.New(),
            "admin@example.com",
            "admin",
            hashPasswordService.HashPassword("123456")
        );

        var user = User.New(
            UserId.New(),
            "user@example.com",
            "user",
            hashPasswordService.HashPassword("123456")
        );

        modelBuilder.Entity<User>().HasData(admin, user);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.HasData(
                new { UsersId = admin.Id, RolesId = roles.First(r => r.Name == RoleNames.Admin).Id },
                new { UsersId = user.Id, RolesId = roles.First(r => r.Name == RoleNames.User).Id }
            ));
    }
}