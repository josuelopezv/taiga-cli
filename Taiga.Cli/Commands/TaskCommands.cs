using Cocona;
using Taiga.Api;
using Taiga.Cli.Configuration;

namespace Taiga.Cli.Commands;

[SubCommand("task", Description = "Commands for managing tasks")]
public class TaskCommands(ITaigaApi _api, IAuthService _authService) : BaseCommand(_authService, _api)
{
    [Command("list", Description = "List tasks (optionally filtered by project or user story)")]
    public async Task ListAsync(
        [Option('p', Description = "Project ID to filter by")] int? project = null,
        [Option('u', Description = "User Story ID to filter by")] int? userStory = null)
    {
        await EnsureAuthenticated();
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
    public async Task GetAsync([Argument(Description = "Task ID")] int id, [Option('p', Description = "Project ID to filter by")] int project)
    {
        await EnsureAuthenticated();
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
        await EnsureAuthenticated();
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
        await EnsureAuthenticated();
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

    [Command("create", Description = "Create a new task")]
    public async Task CreateAsync(
    [Option('p', Description = "Project ID")] int project,
    [Option('t', Description = "Subject/title")] string subject,
    [Option('d', Description = "Description")] string? description = null,
    [Option("status", shortNames: ['s'], Description = "Status ID")] int? status = null,
    [Option("assigned-to", shortNames: ['a'], Description = "Assigned user ID")] int? assignedTo = null,
    [Option('u', Description = "User Story ID (support markdown)")] int? userStory = null,
    [Option("milestone", shortNames: ['m'], Description = "Milestone ID")] int? milestone = null)
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

            if (userStory.HasValue)
            {
                var story = await api.GetUserStoryAsync(userStory.Value, project);
                data["user_story"] = story.Id;
            }

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            var task = await api.CreateTaskAsync(data);
            Console.WriteLine("Task created successfully:");
            Console.WriteLine(task.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating task: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("edit", Description = "Edit a task by ID")]
    public async Task EditAsync(
        [Argument(Description = "Task ID (e.g., 123)")] int refid,
        [Option('p', Description = "Project ID to filter by")] int? project = null,
        [Option('t', Description = "Subject/title")] string? subject = null,
        [Option('d', Description = "Description (support markdown)")] string? description = null,
        [Option("status", shortNames: ['s'], Description = "Status ID")] int? status = null,
        [Option("assigned-to", shortNames: ['a'], Description = "Assigned user ID")] int? assignedTo = null,
        [Option('u', Description = "User Story ID")] int? userStory = null,
        [Option("milestone", shortNames: ['m'], Description = "Milestone ID")] int? milestone = null)
    {
        await EnsureAuthenticated();
        try
        {
            // Get the task by ref to obtain its ID
            var task = await api.GetTaskAsync(refid, project);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (status.HasValue)
                data["status"] = status.Value;

            if (assignedTo.HasValue)
                data["assigned_to"] = assignedTo.Value;

            if (userStory.HasValue)
                data["user_story"] = userStory.Value;

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            if (data.Count == 0)
            {
                Console.WriteLine("No fields to update. Please specify at least one field to modify.");
                return;
            }

            var updatedTask = await api.UpdateTaskAsync(task.Id, data);
            Console.WriteLine("Task updated successfully:");
            Console.WriteLine(updatedTask.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating task: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

