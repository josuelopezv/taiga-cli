using Cocona;
using Taiga.Api;
using Taiga.Cli.Configuration;
using Taiga.Api.Services;

namespace Taiga.Cli.Commands;

[SubCommand("search", Description = "Commands for searching within projects")]
public class SearchCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [PrimaryCommand]
    [Command("project", Description = "Search within a project")]
    public async Task SearchProjectAsync(
        [Argument(Description = "Project ID")] int project,
        [Argument(Description = "Search text")] string text)
    {
        EnsureAuthenticated();
        try
        {
            var results = await api.SearchProjectAsync(project, text);
            Console.WriteLine($"{results.Count} Search Results in Project {project} for '{text}':");
            Console.WriteLine(results);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error performing search: {ex}");
            Environment.Exit(1);
        }
    }
}

