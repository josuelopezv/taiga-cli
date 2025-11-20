using Cocona;
using Microsoft.Extensions.DependencyInjection;
using TaigaCli.Api;
using TaigaCli.Commands;
using TaigaCli.Handlers;
using TaigaCli.Services;

var builder = CoconaApp.CreateBuilder();

// Register services
builder.Services.AddSingleton<AuthService>();
// Register the handler
builder.Services.AddTransient<AuthHeaderHandler>();
// Register factory for creating ITaigaApi with dynamic base URL
builder.Services.AddSingleton<TaigaApiFactory>();
// Register ITaigaApi using factory - creates new instance each time to get latest base URL
builder.Services.AddScoped<ITaigaApi>(sp => sp.GetRequiredService<TaigaApiFactory>().Create());

// Register command classes in DI
builder.Services.AddTransient<AuthCommands>();
builder.Services.AddTransient<ProjectCommands>();
builder.Services.AddTransient<UserStoryCommands>();
builder.Services.AddTransient<TestCommands>();

var app = builder.Build();

// Register commands manually using the service provider
var serviceProvider = app.Services;

app.AddSubCommand("auth", subCommandBuilder =>
{
    var authCommands = serviceProvider.GetRequiredService<AuthCommands>();
    subCommandBuilder.AddCommand("login", authCommands.LoginAsync);
    subCommandBuilder.AddCommand("logout", authCommands.LogoutAsync);
});

app.AddSubCommand("projects", subCommandBuilder =>
{
    var projectCommands = serviceProvider.GetRequiredService<ProjectCommands>();
    subCommandBuilder.AddCommand("list", projectCommands.ListAsync);
});

app.AddSubCommand("stories", subCommandBuilder =>
{
    var storyCommands = serviceProvider.GetRequiredService<UserStoryCommands>();
    subCommandBuilder.AddCommand("list", storyCommands.ListAsync);
});

app.AddSubCommand("test", subCommandBuilder =>
{
    var testCommands = serviceProvider.GetRequiredService<TestCommands>();
    subCommandBuilder.AddCommand("connectivity", testCommands.ConnectivityAsync);
});

app.Run();
