using Cocona.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TaigaCli.Handlers;
using TaigaCli.Services;

namespace TaigaCli.Configuration;

public static class ServiceConfiguration
{
    public static void ConfigureServices(CoconaAppBuilder builder)
    {
        // Core services
        builder.Services
            .AddSingleton<AuthService>()
            .AddTransient<AuthHeaderHandler>()
            .AddSingleton<TaigaApiFactory>()
            // API client - creates new instance each time to get latest base URL
            .AddScoped(sp => sp.GetRequiredService<TaigaApiFactory>().Create())
            .AddHttpClient(TaigaApiFactory.AuthHttpClientName)
            .AddHttpMessageHandler<AuthHeaderHandler>();

        // Register command classes in DI
        foreach (var commandType in CommandDiscovery.DiscoverCommandTypes())
        {
            builder.Services.AddTransient(commandType);
        }

        // Configure logging
        if (builder.Environment.IsDevelopment())
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.SetMinimumLevel(LogLevel.Trace);
        }
    }
}

