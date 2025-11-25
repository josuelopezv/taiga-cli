using Cocona;
using Taiga.Api;
using Taiga.Cli.Configuration;

namespace Taiga.Cli.Commands;

[SubCommand("search", Description = "Commands for searching within projects")]
public class SearchCommands(ITaigaApi _api, IAuthService _authService) : BaseCommand(_authService, _api)
{
    [PrimaryCommand]
    [Command("project", Description = "Search within a project")]
    public async Task SearchProjectAsync(
        [Argument(Description = "Project ID")] int project,
        [Argument(Description = "Search text")] string text)
    {
        await EnsureAuthenticated();
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

