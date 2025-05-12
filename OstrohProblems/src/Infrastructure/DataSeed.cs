using Application.Services.HashPasswordService;
using Domain.Identity;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DataSeed
{
    public static void Seed(ModelBuilder modelBuilder, IHashPasswordService hashPasswordService)
    {
        _seedRoles(modelBuilder);
        _seedUsers(modelBuilder, hashPasswordService);
    }

    private static void _seedRoles(ModelBuilder modelBuilder)
    {
        var roles = new List<Role>();

        foreach (var role in AuthSettings.ListOfRoles)
        {
            roles.Add(Role.New(role));
        }

        modelBuilder.Entity<Role>()
            .HasData(roles);
    }

    private static void _seedUsers(ModelBuilder modelBuilder, IHashPasswordService hashPasswordService)
    {
        var adminRole = Role.New(AuthSettings.AdminRole);
        var userRole = Role.New(AuthSettings.UserRole);

        var adminId = UserId.New();
        var userId = UserId.New();

        var admin = User.New(adminId, "admin@example.com", "admin", hashPasswordService.HashPassword("123456"));
        var user = User.New(userId, "user@example.com", "user", hashPasswordService.HashPassword("123456"));

        modelBuilder.Entity<User>().HasData(admin, user);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.HasData(
                new { UsersId = admin.Id, RolesId = adminRole.Id },
                new { UsersId = user.Id, RolesId = userRole.Id }
            ));
    }
}