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
}

