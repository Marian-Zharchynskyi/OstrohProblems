using Domain.Categories;
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
        var adminUserId = _seedUsers(modelBuilder, roleIds);
        var categoryIds = _seedCategories(modelBuilder);
        var problemIds = _seedProblems(modelBuilder, adminUserId, categoryIds);
        _seedComments(modelBuilder, adminUserId, problemIds);
        _seedRatings(modelBuilder, adminUserId, problemIds);
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

    private static Guid _seedUsers(ModelBuilder modelBuilder, IReadOnlyList<Guid> roles)
    {
        var adminUserId = Guid.Parse("00000000-0000-0000-0000-000000000010");
        var regularUserId = Guid.Parse("00000000-0000-0000-0000-000000000011");
        var coordinatorUserId = Guid.Parse("00000000-0000-0000-0000-000000000012");
        
        var adminRoleId = roles[0];
        var userRoleId = roles[1];
        var coordinatorRoleId = roles[2];

        modelBuilder.Entity<User>().HasData(
            // Password: Admin123!
            new
            {
                Id = new UserId(adminUserId),
                Email = "admin@ostroh.edu.ua",
                FullName = "Адміністратор Острога",
                PasswordHash = "31jGqnyNWeEpqqSOGrrFYA==:pt36JXYoIE3w8xtI8rEJU/h50muKgFwRs0p/h4am3A0="
            },
            // Password: User123!
            new
            {
                Id = new UserId(regularUserId),
                Email = "user@ostroh.edu.ua",
                FullName = "Звичайний Користувач",
                PasswordHash = "OeuuWFIVglEfvDvcH349ow==:OXOk32vZFxUcodlJVPaLj/qOApIGP9SSVu9RBy+O4Sc="
            },
            // Password: Coord123!
            new
            {
                Id = new UserId(coordinatorUserId),
                Email = "coordinator@ostroh.edu.ua",
                FullName = "Координатор Острога",
                PasswordHash = "DXIF+E53jMJS34YZkf0Jkw==:ccAPZCJbWy/stpuqDGYoAeNnHM5rABefB+bMZL+EaZY="
            }
        );

        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.HasData(
                new { UsersId = new UserId(adminUserId), RolesId = new RoleId(adminRoleId) },
                new { UsersId = new UserId(regularUserId), RolesId = new RoleId(userRoleId) },
                new { UsersId = new UserId(coordinatorUserId), RolesId = new RoleId(coordinatorRoleId) }
            ));

        return adminUserId;
    }

    private static IReadOnlyList<Guid> _seedCategories(ModelBuilder modelBuilder)
    {
        var categoryNames = new[]
        {
            "Дороги та тротуари",
            "Освітлення",
            "Благоустрій",
            "Сміття та екологія",
            "Комунальні послуги",
            "Транспорт",
            "Безпека",
            "Парки та зелені зони",
            "Будівництво",
            "Інше"
        };

        var categoryIds = new[]
        {
            Guid.Parse("00000000-0000-0000-0000-000000000101"),
            Guid.Parse("00000000-0000-0000-0000-000000000102"),
            Guid.Parse("00000000-0000-0000-0000-000000000103"),
            Guid.Parse("00000000-0000-0000-0000-000000000104"),
            Guid.Parse("00000000-0000-0000-0000-000000000105"),
            Guid.Parse("00000000-0000-0000-0000-000000000106"),
            Guid.Parse("00000000-0000-0000-0000-000000000107"),
            Guid.Parse("00000000-0000-0000-0000-000000000108"),
            Guid.Parse("00000000-0000-0000-0000-000000000109"),
            Guid.Parse("00000000-0000-0000-0000-00000000010a")
        };

        var categories = categoryNames
            .Select((name, index) => new
            {
                Id = new CategoryId(categoryIds[index]),
                Name = name
            })
            .ToArray();

        modelBuilder.Entity<Category>().HasData(categories);
        return categoryIds;
    }

    private static IReadOnlyList<Guid> _seedProblems(ModelBuilder modelBuilder, Guid adminUserId,
        IReadOnlyList<Guid> categories)
    {
        var statusValues = new[]
        {
            ProblemStatusConstants.New,
            ProblemStatusConstants.InProgress,
            ProblemStatusConstants.Completed,
            ProblemStatusConstants.Rejected
        };

        var problemsData = new[]
        {
            new { Title = "Розбита дорога на вул. Академічна", Lat = 50.3294, Lon = 26.5144, Desc = "Велика яма на дорозі біля будинку №15. Потрібує термінового ремонту.", CategoryIndices = new[] { 0 }, StatusIndex = 0 },
            new { Title = "Не працює вуличне освітлення на вул. Семінарська", Lat = 50.3285, Lon = 26.5125, Desc = "Вже тиждень не світять ліхтарі на ділянці від будинку №10 до №20.", CategoryIndices = new[] { 1 }, StatusIndex = 1 },
            new { Title = "Переповнені сміттєві баки біля ринку", Lat = 50.3301, Lon = 26.5167, Desc = "Сміттєві контейнери не вивозяться вже 3 дні, сміття розкидане навколо.", CategoryIndices = new[] { 3 }, StatusIndex = 0 },
            new { Title = "Зламана лавка в парку Тараса Шевченка", Lat = 50.3278, Lon = 26.5189, Desc = "Дерев'яна лавка зламана, потребує заміни або ремонту.", CategoryIndices = new[] { 2, 7 }, StatusIndex = 2 },
            new { Title = "Аварійне дерево на вул. Луцька", Lat = 50.3312, Lon = 26.5098, Desc = "Старе дерево нахилилося і може впасти на дорогу. Небезпечно для пішоходів та транспорту.", CategoryIndices = new[] { 6, 7 }, StatusIndex = 1 },
            new { Title = "Відсутня розмітка на пішохідному переході", Lat = 50.3289, Lon = 26.5156, Desc = "Пішохідний перехід біля школи №2 без розмітки, небезпечно для дітей.", CategoryIndices = new[] { 0, 6 }, StatusIndex = 0 },
            new { Title = "Тріщина на тротуарі вул. Папаніна", Lat = 50.3305, Lon = 26.5134, Desc = "Великі тріщини на тротуарі, небезпечно для пішоходів, особливо в темний час доби.", CategoryIndices = new[] { 0 }, StatusIndex = 0 },
            new { Title = "Незаконне будівництво на вул. Князів Острозьких", Lat = 50.3298, Lon = 26.5178, Desc = "Ведеться будівництво без відповідних дозволів, порушується архітектурний вигляд міста.", CategoryIndices = new[] { 8 }, StatusIndex = 3 },
            new { Title = "Потребує ремонту дитячий майданчик", Lat = 50.3282, Lon = 26.5142, Desc = "Гойдалки та гірка на дитячому майданчику в аварійному стані.", CategoryIndices = new[] { 2, 6 }, StatusIndex = 1 },
            new { Title = "Відсутній дорожній знак на перехресті", Lat = 50.3307, Lon = 26.5161, Desc = "На перехресті вул. Академічна та вул. Семінарська відсутній знак пріоритету.", CategoryIndices = new[] { 5, 6 }, StatusIndex = 0 },
            new { Title = "Прорив водопроводу на вул. Замкова", Lat = 50.3291, Lon = 26.5171, Desc = "Витік води з водопровідної труби, вода заливає дорогу.", CategoryIndices = new[] { 4 }, StatusIndex = 1 },
            new { Title = "Зарослі бур'яном клумби в центрі", Lat = 50.3296, Lon = 26.5149, Desc = "Клумби біля центральної площі не доглядаються, заросли бур'яном.", CategoryIndices = new[] { 2, 7 }, StatusIndex = 0 },
            new { Title = "Не працює світлофор на вул. Луцька", Lat = 50.3315, Lon = 26.5102, Desc = "Світлофор на перехресті не працює вже другий день.", CategoryIndices = new[] { 5, 6 }, StatusIndex = 1 },
            new { Title = "Сміттєзвалище в лісосмузі", Lat = 50.3272, Lon = 26.5195, Desc = "Стихійне сміттєзвалище утворилося в лісосмузі за містом.", CategoryIndices = new[] { 3 }, StatusIndex = 0 },
            new { Title = "Потребує фарбування огорожа школи", Lat = 50.3288, Lon = 26.5138, Desc = "Огорожа школи №1 іржава та потребує фарбування.", CategoryIndices = new[] { 2 }, StatusIndex = 2 },
            new { Title = "Відсутні урни на зупинці", Lat = 50.3302, Lon = 26.5153, Desc = "На автобусній зупинці 'Центр' немає урн для сміття.", CategoryIndices = new[] { 3, 5 }, StatusIndex = 0 },
            new { Title = "Зламаний бордюр на вул. Князів Острозьких", Lat = 50.3299, Lon = 26.5164, Desc = "Бордюр зруйнований на протяжності 5 метрів.", CategoryIndices = new[] { 0 }, StatusIndex = 0 },
            new { Title = "Потребує обрізки дерева біля будинку", Lat = 50.3286, Lon = 26.5147, Desc = "Гілки дерева загрожують електричним проводам.", CategoryIndices = new[] { 7, 6 }, StatusIndex = 1 },
            new { Title = "Відсутня каналізація на вул. Садова", Lat = 50.3279, Lon = 26.5132, Desc = "Після дощу утворюються великі калюжі через відсутність каналізації.", CategoryIndices = new[] { 4 }, StatusIndex = 0 },
            new { Title = "Граффіті на фасаді історичної будівлі", Lat = 50.3293, Lon = 26.5175, Desc = "Вандали розмалювали фасад історичної будівлі в центрі міста.", CategoryIndices = new[] { 2, 6 }, StatusIndex = 2 }
        };

        var problemIds = new[]
        {
            Guid.Parse("00000000-0000-0000-0000-000000000301"),
            Guid.Parse("00000000-0000-0000-0000-000000000302"),
            Guid.Parse("00000000-0000-0000-0000-000000000303"),
            Guid.Parse("00000000-0000-0000-0000-000000000304"),
            Guid.Parse("00000000-0000-0000-0000-000000000305"),
            Guid.Parse("00000000-0000-0000-0000-000000000306"),
            Guid.Parse("00000000-0000-0000-0000-000000000307"),
            Guid.Parse("00000000-0000-0000-0000-000000000308"),
            Guid.Parse("00000000-0000-0000-0000-000000000309"),
            Guid.Parse("00000000-0000-0000-0000-00000000030a"),
            Guid.Parse("00000000-0000-0000-0000-00000000030b"),
            Guid.Parse("00000000-0000-0000-0000-00000000030c"),
            Guid.Parse("00000000-0000-0000-0000-00000000030d"),
            Guid.Parse("00000000-0000-0000-0000-00000000030e"),
            Guid.Parse("00000000-0000-0000-0000-00000000030f"),
            Guid.Parse("00000000-0000-0000-0000-000000000310"),
            Guid.Parse("00000000-0000-0000-0000-000000000311"),
            Guid.Parse("00000000-0000-0000-0000-000000000312"),
            Guid.Parse("00000000-0000-0000-0000-000000000313"),
            Guid.Parse("00000000-0000-0000-0000-000000000314")
        };

        var baseDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        var problems = problemsData
            .Select((data, index) => new
            {
                Id = new ProblemId(problemIds[index]),
                Title = data.Title,
                Latitude = data.Lat,
                Longitude = data.Lon,
                Description = data.Desc,
                Status = ProblemStatus.From(statusValues[data.StatusIndex]),
                CreatedAt = baseDate,
                UpdatedAt = baseDate,
                CreatedById = new UserId(adminUserId)
            })
            .ToArray();

        modelBuilder.Entity<Problem>().HasData(problems);

        modelBuilder.Entity<Problem>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Problems)
            .UsingEntity(j =>
            {
                var entries = new List<object>();
                for (var i = 0; i < problemsData.Length; i++)
                {
                    var problemId = problemIds[i];
                    foreach (var catIndex in problemsData[i].CategoryIndices)
                    {
                        entries.Add(new
                        {
                            ProblemsId = new ProblemId(problemId),
                            CategoriesId = new CategoryId(categories[catIndex])
                        });
                    }
                }

                j.HasData(entries.ToArray());
            });

        return problemIds;
    }

    private static void _seedComments(ModelBuilder modelBuilder, Guid adminUserId, IReadOnlyList<Guid> problems)
    {
        var commentsData = new[]
        {
            new { ProblemIndex = 0, Content = "Так, підтверджую. Їздив сьогодні, ледь не зламав підвіску." },
            new { ProblemIndex = 0, Content = "Вже місяць як така ситуація. Коли вже відремонтують?" },
            new { ProblemIndex = 1, Content = "Дуже небезпечно ходити ввечері, треба терміново виправити." },
            new { ProblemIndex = 2, Content = "Жахлива ситуація, смердить на всю вулицю." },
            new { ProblemIndex = 3, Content = "Дякую, що виправили!" },
            new { ProblemIndex = 4, Content = "Дійсно небезпечно, особливо при сильному вітрі." },
            new { ProblemIndex = 5, Content = "Діти щодня переходять дорогу, це дуже небезпечно!" },
            new { ProblemIndex = 8, Content = "Коли почнуть ремонт? Діти не можуть нормально гратися." },
            new { ProblemIndex = 10, Content = "Вода вже затопила половину дороги!" },
            new { ProblemIndex = 13, Content = "Це екологічна катастрофа для нашого міста!" }
        };

        var commentIds = new[]
        {
            Guid.Parse("00000000-0000-0000-0000-000000000401"),
            Guid.Parse("00000000-0000-0000-0000-000000000402"),
            Guid.Parse("00000000-0000-0000-0000-000000000403"),
            Guid.Parse("00000000-0000-0000-0000-000000000404"),
            Guid.Parse("00000000-0000-0000-0000-000000000405"),
            Guid.Parse("00000000-0000-0000-0000-000000000406"),
            Guid.Parse("00000000-0000-0000-0000-000000000407"),
            Guid.Parse("00000000-0000-0000-0000-000000000408"),
            Guid.Parse("00000000-0000-0000-0000-000000000409"),
            Guid.Parse("00000000-0000-0000-0000-00000000040a")
        };

        var baseDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        var comments = commentsData
            .Select((data, index) => new
            {
                Id = new CommentId(commentIds[index]),
                Content = data.Content,
                CreatedAt = baseDate,
                UpdatedAt = baseDate,
                ProblemId = new ProblemId(problems[data.ProblemIndex]),
                UserId = new UserId(adminUserId)
            })
            .ToArray();

        modelBuilder.Entity<Comment>().HasData(comments);
    }

    private static void _seedRatings(ModelBuilder modelBuilder, Guid adminUserId, IReadOnlyList<Guid> problems)
    {
        var ratingsData = new[]
        {
            new { ProblemIndex = 0, Points = 4.5 },
            new { ProblemIndex = 1, Points = 5.0 },
            new { ProblemIndex = 2, Points = 4.8 },
            new { ProblemIndex = 4, Points = 5.0 },
            new { ProblemIndex = 5, Points = 4.9 },
            new { ProblemIndex = 6, Points = 4.2 },
            new { ProblemIndex = 9, Points = 4.7 },
            new { ProblemIndex = 10, Points = 5.0 },
            new { ProblemIndex = 13, Points = 4.6 },
            new { ProblemIndex = 16, Points = 4.3 }
        };

        var ratingIds = new[]
        {
            Guid.Parse("00000000-0000-0000-0000-000000000501"),
            Guid.Parse("00000000-0000-0000-0000-000000000502"),
            Guid.Parse("00000000-0000-0000-0000-000000000503"),
            Guid.Parse("00000000-0000-0000-0000-000000000504"),
            Guid.Parse("00000000-0000-0000-0000-000000000505"),
            Guid.Parse("00000000-0000-0000-0000-000000000506"),
            Guid.Parse("00000000-0000-0000-0000-000000000507"),
            Guid.Parse("00000000-0000-0000-0000-000000000508"),
            Guid.Parse("00000000-0000-0000-0000-000000000509"),
            Guid.Parse("00000000-0000-0000-0000-00000000050a")
        };

        var baseDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        var ratings = ratingsData
            .Select((data, index) => new
            {
                Id = new RatingId(ratingIds[index]),
                ProblemId = new ProblemId(problems[data.ProblemIndex]),
                UserId = new UserId(adminUserId),
                Points = data.Points,
                CreatedAt = baseDate
            })
            .ToArray();

        modelBuilder.Entity<Rating>().HasData(ratings);
    }
}