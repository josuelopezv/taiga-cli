using Cocona;
using Taiga.Cli.Configuration;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = CoconaApp.CreateBuilder(args, o => o.EnableShellCompletionSupport = true);
        await ServiceConfiguration.ConfigureServices(builder);
        var app = builder.Build();
        app.RegisterCommandTypes();
        await app.RunAsync();
    }
}