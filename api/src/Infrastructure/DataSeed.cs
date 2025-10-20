using Application.Services.HashPasswordService;
using Domain.Categories;
using Domain.Comments;
using Domain.Identity.Roles;
using Domain.Identity.Users;
using Domain.Problems;
using Domain.Ratings;
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
        var problems = _seedProblems(modelBuilder, users, categories, statuses);
        _seedComments(modelBuilder, problems, users);
        _seedRatings(modelBuilder, problems, users);
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
            "admin@ostroh.edu.ua",
            "Адміністратор Острога",
            hashPasswordService.HashPassword("Admin123!")
        );

        modelBuilder.Entity<User>().HasData(admin);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.HasData(
                new { UsersId = admin.Id, RolesId = roles.First(r => r.Name == RoleNames.Admin).Id }
            ));

        return new List<User> { admin };
    }

    private static List<Category> _seedCategories(ModelBuilder modelBuilder)
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

        var categories = categoryNames
            .Select(name => Category.New(CategoryId.New(), name))
            .ToList();

        modelBuilder.Entity<Category>().HasData(categories);
        return categories;
    }

    private static List<Status> _seedStatuses(ModelBuilder modelBuilder)
    {
        var statusNames = new[]
        {
            "Нова",
            "В роботі",
            "Виконано",
            "Відхилено",
            "Потребує уточнення"
        };

        var statuses = statusNames
            .Select(name => Status.New(StatusId.New(), name))
            .ToList();

        modelBuilder.Entity<Status>().HasData(statuses);
        return statuses;
    }

    private static List<Problem> _seedProblems(ModelBuilder modelBuilder, List<User> users, List<Category> categories,
        List<Status> statuses)
    {
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

        var problems = new List<Problem>();
        var admin = users[0];

        foreach (var data in problemsData)
        {
            var problem = Problem.New(
                ProblemId.New(),
                data.Title,
                latitude: data.Lat,
                longitude: data.Lon,
                description: data.Desc,
                statusId: statuses[data.StatusIndex].Id,
                userId: admin.Id
            );
            problems.Add(problem);
        }

        modelBuilder.Entity<Problem>().HasData(problems);

        // Додаємо зв'язки Problem-Category
        modelBuilder.Entity<Problem>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Problems)
            .UsingEntity(j =>
            {
                var entries = new List<object>();
                for (int i = 0; i < problems.Count; i++)
                {
                    var problemId = problems[i].Id;
                    foreach (var catIndex in problemsData[i].CategoryIndices)
                    {
                        entries.Add(new { ProblemsId = problemId, CategoriesId = categories[catIndex].Id });
                    }
                }
                j.HasData(entries.ToArray());
            });

        return problems;
    }

    private static void _seedComments(ModelBuilder modelBuilder, List<Problem> problems, List<User> users)
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

        var comments = new List<Comment>();
        var admin = users[0];

        foreach (var data in commentsData)
        {
            var comment = Comment.New(
                CommentId.New(),
                data.Content,
                problems[data.ProblemIndex].Id,
                admin.Id
            );
            comments.Add(comment);
        }

        modelBuilder.Entity<Comment>().HasData(comments);
    }

    private static void _seedRatings(ModelBuilder modelBuilder, List<Problem> problems, List<User> users)
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

        var ratings = new List<Rating>();
        var admin = users[0];

        foreach (var data in ratingsData)
        {
            var rating = Rating.New(
                RatingId.New(),
                problems[data.ProblemIndex].Id,
                admin.Id,
                data.Points
            );
            ratings.Add(rating);
        }

        modelBuilder.Entity<Rating>().HasData(ratings);
    }
}