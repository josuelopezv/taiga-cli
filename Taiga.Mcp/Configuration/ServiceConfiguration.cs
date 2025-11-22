using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Taiga.Api.Handlers;
using Taiga.Api.Services;

namespace Taiga.Mcp.Configuration;

public static class ServiceConfiguration
{
    public static void ConfigureServices(HostApplicationBuilder builder)
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

