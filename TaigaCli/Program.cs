global using System.Text.Json.Serialization;
using Cocona;
using TaigaCli.Configuration;

var builder = CoconaApp.CreateBuilder();
// Configure services
ServiceConfiguration.ConfigureServices(builder);
var app = builder.Build();
// Register commands
CommandRegistrar.RegisterCommands(app, app.Services);
app.Run();