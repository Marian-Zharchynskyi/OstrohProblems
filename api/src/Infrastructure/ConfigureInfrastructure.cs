using Infrastructure.Persistence;
using Infrastructure.SignalR;
using Infrastructure.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureInfrastructure
{
    public static void AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddPersistence(builder);
        services.AddSignalRServices();
        services.AddStorageServices(builder);
    }
}