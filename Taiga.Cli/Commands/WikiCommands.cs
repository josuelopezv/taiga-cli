using Cocona;
using Taiga.Api;
using Taiga.Cli.Configuration;

namespace Taiga.Cli.Commands;

[SubCommand("wiki", Description = "Commands for managing wiki pages")]
public class WikiCommands(ITaigaApi _api, IAuthService _authService) : BaseCommand(_authService, _api)
{
    [Command("list", Description = "List wiki pages for a project")]
    public async Task ListAsync([Option('p', Description = "Project ID")] int project)
    {
        await EnsureAuthenticated();
        try
        {
            var pages = await api.GetWikiPagesAsync(project);

            if (pages.Count == 0)
            {
                Console.WriteLine($"No wiki pages found for project {project}.");
                return;
            }

            Console.WriteLine($"Found {pages.Count} wiki page(s):\n");
            foreach (var page in pages)
            {
                Console.WriteLine(page.ToString());
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching wiki pages: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("get", Description = "Get wiki page by ID")]
    public async Task GetAsync([Argument(Description = "Wiki Page ID")] int id)
    {
        await EnsureAuthenticated();
        try
        {
            var page = await api.GetWikiPageAsync(id);
            Console.WriteLine($"Wiki Page Details:");
            Console.WriteLine(page.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching wiki page: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("history", Description = "Get wiki page history")]
    public async Task HistoryAsync([Argument(Description = "Wiki Page ID")] int id)
    {
        await EnsureAuthenticated();
        try
        {
            var history = await api.GetWikiHistoryAsync(id);
            Console.WriteLine($"Wiki Page History (ID: {id}):");
            foreach (var entry in history)
            {
                Console.WriteLine(entry.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching history: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("comments", Description = "Get wiki page comments")]
    public async Task CommentsAsync([Argument(Description = "Wiki Page ID")] int id)
    {
        await EnsureAuthenticated();
        try
        {
            var comments = await api.GetWikiCommentsAsync(id);
            Console.WriteLine($"Wiki Page Comments (ID: {id}):");
            foreach (var comment in comments)
            {
                Console.WriteLine(comment.ToString());
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching comments: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

