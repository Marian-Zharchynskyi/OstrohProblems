using Application.Common.Interfaces;
using Application.Services.GeminiService;
using Infrastructure.Persistence;
using Infrastructure.Services;
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
        
        // Register Clerk API service for syncing users with Clerk Dashboard
        services.AddHttpClient<IClerkApiService, ClerkApiService>();
        
        // Register Gemini AI service
        services.AddHttpClient<IGeminiService, GeminiService>();
    }
}