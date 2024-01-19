using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartCache.Domain.Interfaces;
using SmartCache.Infrastructure.Services;

namespace SmartCache.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICacheService, MicrosoftOrleansService>();
        return services;
    }

    public static IHostBuilder AddInfrastructureHostConfiguratuion(this IHostBuilder host)
    {
        host.UseOrleansClient(client =>
        {
            client.UseLocalhostClustering();
        })
        .ConfigureLogging(logging => logging.AddConsole())
        .UseConsoleLifetime();
        return host;
    }
}
