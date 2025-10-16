using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Services.HashPasswordService;
using Application.Services.ImageService;
using Application.Services.TokenService;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure.Persistence;

public static class ConfigurePersistence
{
    public static void AddPersistence(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var dataSourceBuild =
            new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
        dataSourceBuild.EnableDynamicJson();
        var dataSource = dataSourceBuild.Build();

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(
                    dataSource,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .UseSnakeCaseNamingConvention()
                .ConfigureWarnings(w => w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning)));

        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<StatusRepository>();
        services.AddScoped<IStatusRepository>(provider => provider.GetRequiredService<StatusRepository>());
        services.AddScoped<IStatusQueries>(provider => provider.GetRequiredService<StatusRepository>());

        services.AddScoped<ProblemRepository>();
        services.AddScoped<IProblemRepository>(provider => provider.GetRequiredService<ProblemRepository>());
        services.AddScoped<IProblemQueries>(provider => provider.GetRequiredService<ProblemRepository>());

        services.AddScoped<CategoryRepository>();
        services.AddScoped<ICategoryRepository>(provider => provider.GetRequiredService<CategoryRepository>());
        services.AddScoped<ICategoryQueries>(provider => provider.GetRequiredService<CategoryRepository>());

        services.AddScoped<CommentRepository>();
        services.AddScoped<ICommentRepository>(provider => provider.GetRequiredService<CommentRepository>());
        services.AddScoped<ICommentQueries>(provider => provider.GetRequiredService<CommentRepository>());
        
        services.AddScoped<UserRepository>();
        services.AddScoped<IUserRepository>(provider => provider.GetRequiredService<UserRepository>());
        services.AddScoped<IUserQueries>(provider => provider.GetRequiredService<UserRepository>());
        
        services.AddScoped<RoleRepository>();
        services.AddScoped<IRoleQueries>(provider => provider.GetRequiredService<RoleRepository>());

        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IHashPasswordService, HashPasswordService>();
        services.AddScoped<IImageService, ImageService>();

        services.AddScoped<RefreshTokenRepository>();
        services.AddScoped<IRefreshTokenRepository>(provider => provider.GetRequiredService<RefreshTokenRepository>());
        
        services.AddScoped<RatingRepository>();
        services.AddScoped<IRatingRepository>(provider => provider.GetRequiredService<RatingRepository>());
        services.AddScoped<IRatingQueries>(provider => provider.GetRequiredService<RatingRepository>());
    }
}