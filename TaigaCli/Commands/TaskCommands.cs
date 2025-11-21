using Cocona;
using TaigaCli.Api;
using TaigaCli.Services;

namespace TaigaCli.Commands;

public class TaskCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("list", Description = "List tasks (optionally filtered by project or user story)")]
    public async Task ListAsync(
        [Option('p', Description = "Project ID to filter by")] int? project = null,
        [Option('u', Description = "User Story ID to filter by")] int? userStory = null)
    {
        EnsureAuthenticated();
        try
        {
            var tasks = await api.GetTasksAsync(project, userStory);

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            Console.WriteLine($"Found {tasks.Count} task(s):\n");
            foreach (var task in tasks)
            {
                Console.WriteLine(task.ToString());
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching tasks: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("get", Description = "Get task by ID")]
    public async Task GetAsync([Argument(Description = "Task ID")] int id, [Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
        try
        {
            var task = await api.GetTaskAsync(id, project);
            Console.WriteLine($"Task Details:");
            Console.WriteLine(task.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching task: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("history", Description = "Get task history")]
    public async Task HistoryAsync([Argument(Description = "Task ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var history = await api.GetTaskHistoryAsync(id);
            Console.WriteLine($"Task History (ID: {id}):");
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

    [Command("comments", Description = "Get task comments")]
    public async Task CommentsAsync([Argument(Description = "Task ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var comments = await api.GetTaskCommentsAsync(id);
            Console.WriteLine($"Task Comments (ID: {id}):");
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

    [Command("attachments", Description = "List task attachments")]
    public async Task AttachmentsAsync([Argument(Description = "Task ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var attachments = await api.GetTaskAttachmentsAsync(id);
            Console.WriteLine($"Task Attachments (ID: {id}):");
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

