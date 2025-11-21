using Cocona;
using Microsoft.Extensions.Logging;
using TaigaCli.Api;
using TaigaCli.Services;

namespace TaigaCli.Commands;

public class UserStoryCommands(ITaigaApi api, AuthService authService, ILogger<UserStoryCommands> logger) : BaseCommand(authService)
{
    [Command("list", Description = "List user stories (optionally filtered by project ID)")]
    public async Task ListAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
        try
        {
            var stories = await api.GetUserStoriesAsync(project);

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
    public async Task GetAsync([Argument(Description = "User Story ID")] int id, [Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
        try
        {
            var story = await api.GetUserStoryAsync(id, project);
            Console.WriteLine($"User Story Details:");
            Console.WriteLine(story.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching user story: {ex.Message}");
            logger.LogError(ex, "Error fetching user story with ID {UserStoryId} and Project ID {ProjectId}", id, project);
            Environment.Exit(1);
        }
    }

    [Command("history", Description = "Get user story history")]
    public async Task HistoryAsync([Argument(Description = "User Story ID")] int id)
    {
        EnsureAuthenticated();
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
        EnsureAuthenticated();
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

    [Command("attachments", Description = "List user story attachments")]
    public async Task AttachmentsAsync([Argument(Description = "User Story ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var attachments = await api.GetUserStoryAttachmentsAsync(id);
            Console.WriteLine($"User Story Attachments (ID: {id}):");
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
