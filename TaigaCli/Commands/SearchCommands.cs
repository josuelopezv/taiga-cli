using Cocona;
using TaigaCli.Api;
using TaigaCli.Services;

namespace TaigaCli.Commands;

public class SearchCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("project", Description = "Search within a project")]
    public async Task SearchProjectAsync(
        [Argument(Description = "Project ID")] int project,
        [Argument(Description = "Search text")] string text)
    {
        EnsureAuthenticated();
        try
        {
            var results = await api.SearchProjectAsync(project, text);
            Console.WriteLine($"Search Results in Project {project} for '{text}':");
            foreach (var result in results)
            {
                Console.WriteLine($"  {result.Key}: {result.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error performing search: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

