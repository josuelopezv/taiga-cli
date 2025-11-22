using Cocona;
using TaigaCli.Api;
using TaigaCli.Services;

namespace TaigaCli.Commands;

public class WikiCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("list", Description = "List wiki pages for a project")]
    public async Task ListAsync([Option('p', Description = "Project ID")] int project)
    {
        EnsureAuthenticated();
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
        EnsureAuthenticated();
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
        EnsureAuthenticated();
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
        EnsureAuthenticated();
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

    // DISABLED: Returns 404 error - endpoint not available
    // [Command("attachments", Description = "List wiki page attachments")]
    // public async Task AttachmentsAsync([Argument(Description = "Wiki Page ID")] int id)
    // {
    //     EnsureAuthenticated();
    //     try
    //     {
    //         var attachments = await api.GetWikiAttachmentsAsync(id);
    //         Console.WriteLine($"Wiki Page Attachments (ID: {id}):");
    //         foreach (var attachment in attachments)
    //         {
    //             Console.WriteLine(attachment.ToString());
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"Error fetching attachments: {ex.Message}");
    //         Environment.Exit(1);
    //     }
    // }
}

