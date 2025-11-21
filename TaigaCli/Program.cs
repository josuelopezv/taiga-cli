global using System.Text.Json.Serialization;
using Cocona;
using TaigaCli.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = CoconaApp.CreateBuilder(args);
        ServiceConfiguration.ConfigureServices(builder);
        var app = builder.Build();

        // Register command types as subcommands with custom names
        CommandTypeRegistrar.RegisterCommandTypes(app, app.Services);

        app.Run();
    }
}