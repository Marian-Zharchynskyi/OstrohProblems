using Application.Services.SignalRService;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SignalR;

public static class ConfigureSignalR
{
    public static void AddSignalRServices(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddScoped<ISignalRService, SignalRService>();
    }
}
