using Microsoft.Extensions.DependencyInjection;
using TaigaCli.Handlers;
using TaigaCli.Services;

namespace TaigaCli.Configuration;

public static class ServiceConfiguration
{
    public static void ConfigureServices(IServiceCollection services)
    {
        // Core services
        services.AddSingleton<AuthService>();
        services.AddTransient<AuthHeaderHandler>();
        services.AddSingleton<TaigaApiFactory>();

        // API client - creates new instance each time to get latest base URL
        services.AddScoped(sp => sp.GetRequiredService<TaigaApiFactory>().Create());
    }
}

