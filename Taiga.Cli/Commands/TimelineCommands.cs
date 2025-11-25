using Cocona;
using Taiga.Api;
using Taiga.Cli.Configuration;

namespace Taiga.Cli.Commands;

[SubCommand("timeline", Description = "Commands for viewing timelines")]
public class TimelineCommands(ITaigaApi _api, IAuthService _authService) : BaseCommand(_authService, _api)
{
    [Command("project", Description = "Get project timeline")]
    public async Task ProjectAsync([Argument(Description = "Project ID")] int projectId)
    {
        await EnsureAuthenticated();
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
        await EnsureAuthenticated();
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
        await EnsureAuthenticated();
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

