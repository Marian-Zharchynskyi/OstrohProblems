using Application.Services.HashPasswordService;
using Domain.Categories;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Domain.Problems;
using Domain.Statuses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DataSeed
{
    public static void Seed(ModelBuilder modelBuilder, IHashPasswordService hashPasswordService)
    {
        var roles = _seedRoles(modelBuilder);
        var users = _seedUsers(modelBuilder, hashPasswordService, roles);
        var categories = _seedCategories(modelBuilder);
        var statuses = _seedStatuses(modelBuilder);
        _seedProblems(modelBuilder, users, categories, statuses);
    }

    private static List<Role> _seedRoles(ModelBuilder modelBuilder)
    {
        var roles = new List<Role>
        {
            Role.New(RoleId.New(), RoleNames.Admin),
            Role.New(RoleId.New(), RoleNames.User)
        };

        modelBuilder.Entity<Role>().HasData(roles);
        return roles;
    }

    private static List<User> _seedUsers(ModelBuilder modelBuilder, IHashPasswordService hashPasswordService,
        List<Role> roles)
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

        return new List<User> { admin, user };
    }

    private static List<Category> _seedCategories(ModelBuilder modelBuilder)
    {
        var categories = Enumerable.Range(1, 10)
            .Select(i => Category.New(CategoryId.New(), $"Category {i}"))
            .ToList();

        modelBuilder.Entity<Category>().HasData(categories);
        return categories;
    }

    private static List<Status> _seedStatuses(ModelBuilder modelBuilder)
    {
        var statuses = Enumerable.Range(1, 10)
            .Select(i => Status.New(StatusId.New(), $"Status {i}"))
            .ToList();

        modelBuilder.Entity<Status>().HasData(statuses);
        return statuses;
    }

    private static void _seedProblems(ModelBuilder modelBuilder, List<User> users, List<Category> categories,
        List<Status> statuses)
    {
        var problems = new List<Problem>();

        for (int i = 1; i <= 10; i++)
        {
            var problem = Problem.New(
                ProblemId.New(),
                $"Problem {i}",
                latitude: 50 + i * 0.1,
                longitude: 30 + i * 0.1,
                description: $"Description of problem {i}",
                statusId: statuses[i % statuses.Count].Id,
                userId: users[i % users.Count].Id
            );

            problems.Add(problem);
        }

        modelBuilder.Entity<Problem>().HasData(problems);

        modelBuilder.Entity<Problem>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Problems)
            .UsingEntity(j =>
            {
                var entries = new List<object>();
                for (int i = 0; i < problems.Count; i++)
                {
                    var problemId = problems[i].Id;
                    var category1 = categories[i % categories.Count].Id;
                    var category2 = categories[(i + 1) % categories.Count].Id;

                    entries.Add(new { ProblemsId = problemId, CategoriesId = category1 });
                    entries.Add(new { ProblemsId = problemId, CategoriesId = category2 });
                }

                j.HasData(entries.ToArray());
            });
    }
}