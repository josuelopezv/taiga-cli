using Cocona;
using TaigaCli.Api;
using TaigaCli.Services;

namespace TaigaCli.Commands;

public class UserStoryCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
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
                Console.WriteLine($"  ID: {story.Id}");
                Console.WriteLine($"  Subject: {story.Subject}");
                Console.WriteLine($"  Project: {story.Project}");
                Console.WriteLine($"  Status: {story.Status}");
                if (!string.IsNullOrWhiteSpace(story.Description))
                {
                    Console.WriteLine($"  Description: {story.Description}");
                }
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
    public async Task GetAsync([Argument(Description = "User Story ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var story = await api.GetUserStoryAsync(id);
            Console.WriteLine($"User Story Details:");
            Console.WriteLine($"  ID: {story.Id}");
            Console.WriteLine($"  Subject: {story.Subject}");
            Console.WriteLine($"  Project: {story.Project}");
            Console.WriteLine($"  Status: {story.Status}");
            if (!string.IsNullOrWhiteSpace(story.Description))
            {
                Console.WriteLine($"  Description: {story.Description}");
            }
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
        EnsureAuthenticated();
        try
        {
            var history = await api.GetUserStoryHistoryAsync(id);
            Console.WriteLine($"User Story History (ID: {id}):");
            foreach (var entry in history)
            {
                Console.WriteLine($"  Entry {entry.Id}: {entry.CreatedAt} - Type: {entry.Type}");
                if (!string.IsNullOrWhiteSpace(entry.Comment))
                {
                    Console.WriteLine($"    Comment: {entry.Comment}");
                }
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
                Console.WriteLine($"  Comment {comment.Id}: {comment.CreatedDate}");
                Console.WriteLine($"    {comment.CommentText}");
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
                Console.WriteLine($"  ID: {attachment.Id}, Name: {attachment.Name}, Size: {attachment.Size} bytes");
                Console.WriteLine($"    URL: {attachment.Url}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching attachments: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
