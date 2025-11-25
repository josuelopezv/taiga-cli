using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Taiga.Mcp.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Logging.AddConsole(consoleLogOptions => consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Debug);

        builder.Services
            .AddMcpServer()
            .WithStdioServerTransport()
            .WithToolsFromAssembly();

        // Core services
        builder.Services
            .AddSingleton<IAuthService, AuthService>()
            .AddSingleton<TaigaApiFactory>()
            // API client - creates new instance each time to get latest base URL
            .AddScoped(async sp => sp.GetRequiredService<TaigaApiFactory>().Create());

        await builder.Build().RunAsync();
    }
}