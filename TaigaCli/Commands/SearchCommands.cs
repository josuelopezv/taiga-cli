using Cocona;
using TaigaCli.Api;
using TaigaCli.Services;

namespace TaigaCli.Commands;

public class SearchCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("search", Description = "Search across projects")]
    public async Task SearchAsync([Argument(Description = "Search text")] string text)
    {
        EnsureAuthenticated();
        try
        {
            var results = await api.SearchAsync(text);
            Console.WriteLine($"Search Results for '{text}':");
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

    [Command("project", Description = "Search within a project")]
    public async Task SearchProjectAsync(
        [Argument(Description = "Project ID")] int projectId,
        [Argument(Description = "Search text")] string text)
    {
        EnsureAuthenticated();
        try
        {
            var results = await api.SearchProjectAsync(projectId, text);
            Console.WriteLine($"Search Results in Project {projectId} for '{text}':");
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

