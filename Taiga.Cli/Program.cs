using Cocona;
using Taiga.Cli.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = CoconaApp.CreateBuilder(args, o => o.EnableShellCompletionSupport = true);
        ServiceConfiguration.ConfigureServices(builder);
        var app = builder.Build();
        app.RegisterCommandTypes();
        app.Run();
    }
}