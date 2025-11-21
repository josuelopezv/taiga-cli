global using System.Text.Json.Serialization;
using Cocona;
using TaigaCli.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = CoconaApp.CreateBuilder(args);
        // Configure services
        ServiceConfiguration.ConfigureServices(builder);
        var app = builder.Build();
        // Register commands
        CommandRegistrar.RegisterCommands(app, app.Services);
        app.Run();
    }
}