using DI.Console.Services;
using DI.Console.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Console.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IConsoleService, ConsoleService>();
        return services;
    }

    public static T GetRequiredServices<T>(this IServiceProvider services)
    {
        using IServiceScope serviceScope = services.CreateScope();
        IServiceProvider provider = serviceScope.ServiceProvider;

        return provider.GetRequiredService<T>();
    }
}
