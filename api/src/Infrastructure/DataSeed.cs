using Domain.Comments;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Domain.Problems;
using Domain.Ratings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DataSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var roleIds = _seedRoles(modelBuilder);
        var userIds = _seedUsers(modelBuilder, roleIds);
        var problemIds = _seedProblems(modelBuilder, userIds);
        _seedComments(modelBuilder, problemIds, userIds);
        _seedRatings(modelBuilder, problemIds, userIds);
    }

    private static IReadOnlyList<Guid> _seedRoles(ModelBuilder modelBuilder)
    {
        var adminRoleId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        var userRoleId = Guid.Parse("00000000-0000-0000-0000-000000000002");
        var coordinatorRoleId = Guid.Parse("00000000-0000-0000-0000-000000000003");

        modelBuilder.Entity<Role>().HasData(
            new { Id = new RoleId(adminRoleId), Name = RoleNames.Admin },
            new { Id = new RoleId(userRoleId), Name = RoleNames.User },
            new { Id = new RoleId(coordinatorRoleId), Name = RoleNames.Coordinator }
        );

        return new[] { adminRoleId, userRoleId, coordinatorRoleId };
    }

    private static IReadOnlyList<Guid> _seedUsers(ModelBuilder modelBuilder, IReadOnlyList<Guid> roles)
    {
        var adminUserId = Guid.Parse("00000000-0000-0000-0000-000000000010");
        var regularUserId = Guid.Parse("00000000-0000-0000-0000-000000000011");
        var coordinatorUserId = Guid.Parse("00000000-0000-0000-0000-000000000012");
        
        var adminRoleId = roles[0];
        var userRoleId = roles[1];
        var coordinatorRoleId = roles[2];

        modelBuilder.Entity<User>().HasData(
            // Password: Admin123!Ostroh
            new
            {
                Id = new UserId(adminUserId),
                ClerkId = "user_38GDF8VSzp5vQGWOBgk0Kv3Td7W",
                Email = "admin@ostroh.edu.ua",
                Name = "Адміністратор",
                Surname = "Острога",
                PasswordHash = "31jGqnyNWeEpqqSOGrrFYA==:pt36JXYoIE3w8xtI8rEJU/h50muKgFwRs0p/h4am3A0=",
                RoleId = new RoleId(adminRoleId)
            },
            // Password: User123!Ostroh
            new
            {
                Id = new UserId(regularUserId),
                ClerkId = "user_38GDs4xMAfETf7thv21MwyW72bh",
                Email = "user@ostroh.edu.ua",
                Name = "Звичайний",
                Surname = "Користувач",
                PasswordHash = "OeuuWFIVglEfvDvcH349ow==:OXOk32vZFxUcodlJVPaLj/qOApIGP9SSVu9RBy+O4Sc=",
                RoleId = new RoleId(userRoleId)
            },
            // Password: Coord123!Ostroh
            new
            {
                Id = new UserId(coordinatorUserId),
                ClerkId = "user_38GDlYzZ1LZNY3q4uFLBMyAYp4Y",
                Email = "coordinator@ostroh.edu.ua",
                Name = "Координатор",
                Surname = "Острога",
                PasswordHash = "DXIF+E53jMJS34YZkf0Jkw==:ccAPZCJbWy/stpuqDGYoAeNnHM5rABefB+bMZL+EaZY=",
                RoleId = new RoleId(coordinatorRoleId)
            }
        );

        return new[] { adminUserId, regularUserId, coordinatorUserId };
    }

    private static IReadOnlyList<Guid> _seedProblems(ModelBuilder modelBuilder, IReadOnlyList<Guid> userIds)
    {
        var regularUserId = userIds[1];
        var baseDate = new DateTime(2024, 1, 15, 10, 0, 0, DateTimeKind.Utc);

        var problemsData = new[]
        {
            new { Id = Guid.Parse("00000000-0000-0000-0001-000000000001"), Title = "Розбита дорога на вул. Академічна", Lat = 50.3294, Lon = 26.5144, Desc = "Велика яма на дорозі біля будинку №15. Потребує термінового ремонту.", Status = ProblemStatus.New, Priority = Priority.High, Categories = new List<Category> { Category.Roads } },
            new { Id = Guid.Parse("00000000-0000-0000-0001-000000000002"), Title = "Не працює вуличне освітлення на вул. Семінарська", Lat = 50.3285, Lon = 26.5125, Desc = "Вже тиждень не світять ліхтарі на ділянці від будинку №5 до №15.", Status = ProblemStatus.New, Priority = Priority.Medium, Categories = new List<Category> { Category.Lighting } },
            new { Id = Guid.Parse("00000000-0000-0000-0001-000000000003"), Title = "Переповнені сміттєві баки біля ринку", Lat = 50.3301, Lon = 26.5167, Desc = "Сміттєві контейнери не вивозяться вже 3 дні, сміття розкидане навколо.", Status = ProblemStatus.New, Priority = Priority.High, Categories = new List<Category> { Category.Garbage } },
            new { Id = Guid.Parse("00000000-0000-0000-0001-000000000004"), Title = "Аварійне дерево на вул. Луцька", Lat = 50.3312, Lon = 26.5098, Desc = "Старе дерево нахилилося і може впасти на дорогу. Небезпечно для пішоходів.", Status = ProblemStatus.InProgress, Priority = Priority.Critical, Categories = new List<Category> { Category.Safety, Category.Parks } },
            new { Id = Guid.Parse("00000000-0000-0000-0001-000000000005"), Title = "Відсутня розмітка на пішохідному переході", Lat = 50.3289, Lon = 26.5156, Desc = "Пішохідний перехід біля школи №2 без розмітки, небезпечно для дітей.", Status = ProblemStatus.InProgress, Priority = Priority.High, Categories = new List<Category> { Category.Roads, Category.Safety } },
            new { Id = Guid.Parse("00000000-0000-0000-0001-000000000006"), Title = "Тріщина на тротуарі вул. Папаніна", Lat = 50.3305, Lon = 26.5134, Desc = "Великі тріщини на тротуарі, небезпечно для пішоходів та велосипедистів.", Status = ProblemStatus.New, Priority = Priority.Medium, Categories = new List<Category> { Category.Roads } },
            new { Id = Guid.Parse("00000000-0000-0000-0001-000000000007"), Title = "Потребує ремонту дитячий майданчик", Lat = 50.3282, Lon = 26.5142, Desc = "Гойдалки та гірка на дитячому майданчику в аварійному стані.", Status = ProblemStatus.InProgress, Priority = Priority.High, Categories = new List<Category> { Category.Safety, Category.Parks } },
            new { Id = Guid.Parse("00000000-0000-0000-0001-000000000008"), Title = "Прорив водопроводу на вул. Замкова", Lat = 50.3291, Lon = 26.5171, Desc = "Витік води з водопровідної труби, вода заливає дорогу.", Status = ProblemStatus.New, Priority = Priority.Critical, Categories = new List<Category> { Category.Water } },
            new { Id = Guid.Parse("00000000-0000-0000-0001-000000000009"), Title = "Зарослі бур'яном клумби в центрі", Lat = 50.3296, Lon = 26.5149, Desc = "Клумби біля центральної площі не доглядаються, заросли бур'яном.", Status = ProblemStatus.New, Priority = Priority.Low, Categories = new List<Category> { Category.Parks } },
            new { Id = Guid.Parse("00000000-0000-0000-0001-000000000010"), Title = "Не працює світлофор на вул. Луцька", Lat = 50.3315, Lon = 26.5102, Desc = "Світлофор на перехресті не працює вже другий день, створює аварійну ситуацію.", Status = ProblemStatus.Rejected, Priority = Priority.Critical, Categories = new List<Category> { Category.Safety, Category.Roads } },
        };

        var problemIds = new List<Guid>();

        foreach (var p in problemsData)
        {
            problemIds.Add(p.Id);
            modelBuilder.Entity<Problem>().HasData(new
            {
                Id = new ProblemId(p.Id),
                Title = p.Title,
                Latitude = p.Lat,
                Longitude = p.Lon,
                Description = p.Desc,
                Status = p.Status,
                Priority = p.Priority,
                CreatedAt = baseDate,
                UpdatedAt = baseDate,
                CreatedById = new UserId(regularUserId),
                Categories = p.Categories
            });
        }

        return problemIds;
    }

    private static void _seedComments(ModelBuilder modelBuilder, IReadOnlyList<Guid> problemIds, IReadOnlyList<Guid> userIds)
    {
        var regularUserId = userIds[1];
        var coordinatorUserId = userIds[2];
        var baseDate = new DateTime(2024, 1, 16, 14, 0, 0, DateTimeKind.Utc);

        var commentsData = new[]
        {
            new { ProblemIndex = 0, Content = "Підтверджую, яма дуже глибока, пошкодив колесо.", UserId = regularUserId },
            new { ProblemIndex = 0, Content = "Дякуємо за звернення, передали в дорожню службу.", UserId = coordinatorUserId },
            new { ProblemIndex = 1, Content = "Темно ввечері ходити, небезпечно.", UserId = regularUserId },
            new { ProblemIndex = 2, Content = "Жахливий запах, неможливо пройти повз.", UserId = regularUserId },
            new { ProblemIndex = 3, Content = "Прошу терміново вирішити, гілки вже торкаються проводів.", UserId = regularUserId },
            new { ProblemIndex = 4, Content = "Діти переходять дорогу в небезпечному місці.", UserId = regularUserId },
            new { ProblemIndex = 7, Content = "Вода тече вже третій день, ніхто не реагує.", UserId = regularUserId },
        };

        var commentId = 1;
        foreach (var c in commentsData)
        {
            modelBuilder.Entity<Comment>().HasData(new
            {
                Id = new CommentId(Guid.Parse($"00000000-0000-0000-0002-00000000000{commentId}")),
                Content = c.Content,
                CreatedAt = baseDate.AddHours(commentId),
                UpdatedAt = baseDate.AddHours(commentId),
                ProblemId = new ProblemId(problemIds[c.ProblemIndex]),
                UserId = new UserId(c.UserId)
            });
            commentId++;
        }
    }

    private static void _seedRatings(ModelBuilder modelBuilder, IReadOnlyList<Guid> problemIds, IReadOnlyList<Guid> userIds)
    {
        var regularUserId = userIds[1];
        var adminUserId = userIds[0];
        var baseDate = new DateTime(2024, 1, 17, 10, 0, 0, DateTimeKind.Utc);

        var ratingsData = new[]
        {
            new { ProblemIndex = 0, Points = 5.0, UserId = regularUserId },
            new { ProblemIndex = 0, Points = 4.0, UserId = adminUserId },
            new { ProblemIndex = 1, Points = 4.0, UserId = regularUserId },
            new { ProblemIndex = 2, Points = 5.0, UserId = regularUserId },
            new { ProblemIndex = 3, Points = 5.0, UserId = regularUserId },
            new { ProblemIndex = 4, Points = 4.0, UserId = regularUserId },
            new { ProblemIndex = 5, Points = 3.0, UserId = regularUserId },
            new { ProblemIndex = 7, Points = 5.0, UserId = regularUserId },
            new { ProblemIndex = 9, Points = 5.0, UserId = regularUserId },
        };

        var ratingId = 1;
        foreach (var r in ratingsData)
        {
            modelBuilder.Entity<Rating>().HasData(new
            {
                Id = new RatingId(Guid.Parse($"00000000-0000-0000-0003-00000000000{ratingId}")),
                ProblemId = new ProblemId(problemIds[r.ProblemIndex]),
                UserId = new UserId(r.UserId),
                Points = r.Points,
                CreatedAt = baseDate.AddHours(ratingId)
            });
            ratingId++;
        }
    }
}