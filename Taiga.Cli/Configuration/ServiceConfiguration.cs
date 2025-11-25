using Cocona.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Taiga.Cli.Services;

namespace Taiga.Cli.Configuration;

public static class ServiceConfiguration
{
    public static async Task ConfigureServices(CoconaAppBuilder builder)
    {
        // Core services
        builder.Services
            .AddSingleton<ConfigService>()
            .AddSingleton<IAuthService, AuthService>()
            .AddSingleton<TaigaApiFactory>()
            // API client - creates new instance each time to get latest base URL
            .AddScoped(sp => sp.GetRequiredService<TaigaApiFactory>().Create());

        // Register command classes in DI
        foreach (var commandType in CommandDiscovery.DiscoverCommandTypes())
            builder.Services.AddTransient(commandType);

        // Configure logging
        builder.Logging.ClearProviders();
        if (builder.Environment.IsDevelopment())
        {
            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(LogLevel.Trace);
            Console.WriteLine("Logging level set to Trace for Development environment.");
        }
    }
}

