using Microsoft.Extensions.DependencyInjection;
using SmartCache.Application.Services;
using SmartCache.Domain.Interfaces;

namespace SmartCache.Application;
public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailBreachService, EmailBreachService>();
        return services;
    }
}
