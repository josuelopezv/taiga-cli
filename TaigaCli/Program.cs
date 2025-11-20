using Cocona;
using Microsoft.Extensions.DependencyInjection;
using TaigaCli.Configuration;

var builder = CoconaApp.CreateBuilder();

// Configure services
ServiceConfiguration.ConfigureServices(builder.Services);

// Register command classes in DI
foreach (var commandType in CommandDiscovery.DiscoverCommandTypes())
{
    builder.Services.AddTransient(commandType);
}

var app = builder.Build();

// Register commands
CommandRegistrar.RegisterCommands(app, app.Services);

app.Run();