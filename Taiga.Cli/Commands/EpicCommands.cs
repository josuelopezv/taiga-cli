using Cocona;
using Taiga.Api;
using Taiga.Cli.Configuration;

namespace Taiga.Cli.Commands;

[SubCommand("epic", Description = "Commands for managing epics")]
public class EpicCommands(ITaigaApi _api, IAuthService _authService) : BaseCommand(_authService, _api)
{
    [Command("list", Description = "List epics (optionally filtered by project)")]
    public async Task ListAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        await EnsureAuthenticated();
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
    public async Task GetAsync([Argument(Description = "Epic ID")] int id, [Option('p', Description = "Project ID to filter by")] int project)
    {
        await EnsureAuthenticated();
        try
        {
            var epic = await api.GetEpicAsync(id, project);
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
        await EnsureAuthenticated();
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
        await EnsureAuthenticated();
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
        await EnsureAuthenticated();
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

    [Command("create", Description = "Create a new epic")]
    public async Task CreateAsync(
        [Option('p', Description = "Project ID")] int project,
        [Option('t', Description = "Subject/title")] string subject,
        [Option('d', Description = "Description (support markdown)")] string? description = null,
        [Option("status", shortNames: ['s'], Description = "Status ID")] int? status = null,
        [Option("assigned-to", shortNames: ['a'], Description = "Assigned user ID")] int? assignedTo = null)
    {
        await EnsureAuthenticated();
        try
        {
            var data = new Dictionary<string, object>
            {
                ["project"] = project,
                ["subject"] = subject
            };

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (status.HasValue)
                data["status"] = status.Value;

            if (assignedTo.HasValue)
                data["assigned_to"] = assignedTo.Value;

            var epic = await api.CreateEpicAsync(data);
            Console.WriteLine("Epic created successfully:");
            Console.WriteLine(epic.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating epic: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("edit", Description = "Edit an epic by ID")]
    public async Task EditAsync(
        [Argument(Description = "Epic ID (e.g., 123)")] int refid,
        [Option('p', Description = "Project ID to filter by")] int? project = null,
        [Option('t', Description = "Subject/title")] string? subject = null,
        [Option('d', Description = "Description (support markdown)")] string? description = null,
        [Option("status", shortNames: ['s'], Description = "Status ID")] int? status = null,
        [Option("assigned-to", shortNames: ['a'], Description = "Assigned user ID")] int? assignedTo = null)
    {
        await EnsureAuthenticated();
        try
        {
            // Get the epic by ref to obtain its ID
            var epic = await api.GetEpicAsync(refid, project);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (status.HasValue)
                data["status"] = status.Value;

            if (assignedTo.HasValue)
                data["assigned_to"] = assignedTo.Value;

            if (data.Count == 0)
            {
                Console.WriteLine("No fields to update. Please specify at least one field to modify.");
                return;
            }

            var updatedEpic = await api.UpdateEpicAsync(epic.Id, data);
            Console.WriteLine("Epic updated successfully:");
            Console.WriteLine(updatedEpic.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating epic: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

