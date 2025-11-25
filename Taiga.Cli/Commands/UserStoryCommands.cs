using Cocona;
using Taiga.Api;
using Taiga.Cli.Configuration;

namespace Taiga.Cli.Commands;

[SubCommand("userstory", Description = "Commands for managing user stories")]
public class UserStoryCommands(ITaigaApi _api, IAuthService _authService) : BaseCommand(_authService, _api)
{
    [Command("list", Description = "List user stories (optionally filtered by project ID)")]
    public async Task ListAsync([Option('p', Description = "Project ID to filter by")] int? project = null,
                                [Option('e', Description = "Epic ID to filter by")] int? epic = null)
    {
        await EnsureAuthenticated();
        try
        {
            var stories = await api.GetUserStoriesAsync(project, epic);

            if (stories.Count == 0)
            {
                Console.WriteLine(project.HasValue
                    ? $"No user stories found for project {project}."
                    : "No user stories found.");
                return;
            }

            Console.WriteLine($"Found {stories.Count} user story/stories:\n");
            foreach (var story in stories)
            {
                Console.WriteLine(story.ToString());
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching user stories: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("get", Description = "Get user story by ID")]
    public async Task GetAsync([Argument(Description = "User Story ID")] int id, [Option('p', Description = "Project ID to filter by")] int project)
    {
        await EnsureAuthenticated();
        try
        {
            var story = await api.GetUserStoryAsync(id, project);
            Console.WriteLine($"User Story Details:");
            Console.WriteLine(story.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching user story: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("history", Description = "Get user story history")]
    public async Task HistoryAsync([Argument(Description = "User Story ID")] int id)
    {
        await EnsureAuthenticated();
        try
        {
            var history = await api.GetUserStoryHistoryAsync(id);
            Console.WriteLine($"User Story History (ID: {id}):");
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

    [Command("comments", Description = "Get user story comments")]
    public async Task CommentsAsync([Argument(Description = "User Story ID")] int id)
    {
        await EnsureAuthenticated();
        try
        {
            var comments = await api.GetUserStoryCommentsAsync(id);
            Console.WriteLine($"User Story Comments (ID: {id}):");
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

    [Command("create", Description = "Create a new user story")]
    public async Task CreateAsync(
        [Option('p', Description = "Project ID")] int project,
        [Option('t', Description = "Subject/title")] string subject,
        [Option('d', Description = "Description (support markdown)")] string? description = null,
        [Option("status", shortNames: ['s'], Description = "Status ID")] int? status = null,
        [Option("assigned-to", shortNames: ['a'], Description = "Assigned user ID")] int? assignedTo = null,
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

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            var story = await api.CreateUserStoryAsync(data);
            Console.WriteLine("User story created successfully:");
            Console.WriteLine(story.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating user story: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("edit", Description = "Edit a user story by ID")]
    public async Task EditAsync(
        [Argument(Description = "User Story ID (e.g., 123)")] int refid,
        [Option('p', Description = "Project ID to filter by")] int? project = null,
        [Option('t', Description = "Subject/title")] string? subject = null,
        [Option('d', Description = "Description (support markdown)")] string? description = null,
        [Option("status", shortNames: ['s'], Description = "Status ID")] int? status = null,
        [Option("assigned-to", shortNames: ['a'], Description = "Assigned user ID")] int? assignedTo = null,
        [Option("milestone", shortNames: ['m'], Description = "Milestone ID")] int? milestone = null)
    {
        await EnsureAuthenticated();
        try
        {
            // Get the user story by ref to obtain its ID
            var story = await api.GetUserStoryAsync(refid, project);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (status.HasValue)
                data["status"] = status.Value;

            if (assignedTo.HasValue)
                data["assigned_to"] = assignedTo.Value;

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            if (data.Count == 0)
            {
                Console.WriteLine("No fields to update. Please specify at least one field to modify.");
                return;
            }

            var updatedStory = await api.UpdateUserStoryAsync(story.Id, data);
            Console.WriteLine("User story updated successfully:");
            Console.WriteLine(updatedStory.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating user story: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
