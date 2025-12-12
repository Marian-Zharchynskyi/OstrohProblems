using Amazon.S3;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Storage;

public static class ConfigureStorage
{
    public static void AddStorageServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var settings = builder.Configuration.GetSection(BackblazeB2Settings.SectionName);
        services.Configure<BackblazeB2Settings>(settings);

        var b2Settings = settings.Get<BackblazeB2Settings>()!;

        var config = new AmazonS3Config
        {
            ServiceURL = $"https://{b2Settings.Endpoint}",
            ForcePathStyle = true
        };

        var s3Client = new AmazonS3Client(
            b2Settings.KeyId,
            b2Settings.ApplicationKey,
            config);

        services.AddSingleton<IAmazonS3>(s3Client);
        services.AddScoped<IStorageService, BackblazeB2StorageService>();
    }
}
