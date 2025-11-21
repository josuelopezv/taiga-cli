using Cocona;
using TaigaCli.Api;
using TaigaCli.Services;

namespace TaigaCli.Commands;

public class EpicCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("list", Description = "List epics (optionally filtered by project)")]
    public async Task ListAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
        try
        {
            var epics = await api.GetEpicsAsync(project);

            if (epics.Count == 0)
            {
                Console.WriteLine("No epics found.");
                return;
            }

            Console.WriteLine($"Found {epics.Count} epic(s):\n");
            foreach (var epic in epics)
            {
                Console.WriteLine(epic.ToString());
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching epics: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("get", Description = "Get epic by ID")]
    public async Task GetAsync([Argument(Description = "Epic ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var epic = await api.GetEpicAsync(id);
            Console.WriteLine($"Epic Details:");
            Console.WriteLine(epic.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching epic: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("history", Description = "Get epic history")]
    public async Task HistoryAsync([Argument(Description = "Epic ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var history = await api.GetEpicHistoryAsync(id);
            Console.WriteLine($"Epic History (ID: {id}):");
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

    [Command("related-stories", Description = "Get related user stories")]
    public async Task RelatedStoriesAsync([Argument(Description = "Epic ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var stories = await api.GetEpicRelatedUserStoriesAsync(id);
            Console.WriteLine($"Related User Stories (Epic ID: {id}):");
            foreach (var story in stories)
            {
                Console.WriteLine(story.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching related stories: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("comments", Description = "Get epic comments")]
    public async Task CommentsAsync([Argument(Description = "Epic ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var comments = await api.GetEpicCommentsAsync(id);
            Console.WriteLine($"Epic Comments (ID: {id}):");
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

    [Command("attachments", Description = "List epic attachments")]
    public async Task AttachmentsAsync([Argument(Description = "Epic ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var attachments = await api.GetEpicAttachmentsAsync(id);
            Console.WriteLine($"Epic Attachments (ID: {id}):");
            foreach (var attachment in attachments)
            {
                Console.WriteLine(attachment.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching attachments: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

