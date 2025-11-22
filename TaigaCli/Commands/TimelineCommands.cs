using Cocona;
using TaigaCli.Api;
using TaigaCli.Configuration;
using TaigaCli.Services;

namespace TaigaCli.Commands;

[SubCommand("timeline", Description = "Commands for viewing timelines")]
public class TimelineCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("project", Description = "Get project timeline")]
    public async Task ProjectAsync([Argument(Description = "Project ID")] int projectId)
    {
        EnsureAuthenticated();
        try
        {
            var timeline = await api.GetProjectTimelineAsync(projectId);
            Console.WriteLine($"Project Timeline (Project ID: {projectId}):");
            foreach (var entry in timeline)
            {
                Console.WriteLine($"  Entry {entry.Id}: {entry.CreatedAt} - Type: {entry.Type}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching timeline: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("profile", Description = "Get profile timeline")]
    public async Task ProfileAsync([Argument(Description = "Project ID")] int projectId)
    {
        EnsureAuthenticated();
        try
        {
            var timeline = await api.GetProfileTimelineAsync(projectId);
            Console.WriteLine($"Profile Timeline (Project ID: {projectId}):");
            foreach (var entry in timeline)
            {
                Console.WriteLine($"  Entry {entry.Id}: {entry.CreatedAt} - Type: {entry.Type}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching timeline: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("user", Description = "Get user timeline")]
    public async Task UserAsync(
        [Argument(Description = "Project ID")] int projectId,
        [Argument(Description = "User ID")] int userId)
    {
        EnsureAuthenticated();
        try
        {
            var timeline = await api.GetUserTimelineAsync(projectId, userId);
            Console.WriteLine($"User Timeline (Project ID: {projectId}, User ID: {userId}):");
            foreach (var entry in timeline)
            {
                Console.WriteLine($"  Entry {entry.Id}: {entry.CreatedAt} - Type: {entry.Type}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching timeline: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

