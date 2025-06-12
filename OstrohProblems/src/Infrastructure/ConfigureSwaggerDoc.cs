using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Infrastructure;

public static class ConfigureSwaggerDoc
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "Ostroh API",
                Version = "v1",
                Description = "API для управління проблемами та рішеннями на платформі Ostroh Problems",
            });
        });
    }
}
