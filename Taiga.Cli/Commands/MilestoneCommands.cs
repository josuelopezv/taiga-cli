using Cocona;
using Taiga.Api;
using Taiga.Cli.Configuration;

namespace Taiga.Cli.Commands;

[SubCommand("milestone", Description = "Commands for managing milestones")]
public class MilestoneCommands(ITaigaApi _api, IAuthService _authService) : BaseCommand(_authService, _api)
{
    [Command("list", Description = "List milestones (optionally filtered by project)")]
    public async Task ListAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        await EnsureAuthenticated();
        try
        {
            var milestones = await api.GetMilestonesAsync(project);

            if (milestones.Count == 0)
            {
                Console.WriteLine("No milestones found.");
                return;
            }

            Console.WriteLine($"Found {milestones.Count} milestone(s):\n");
            foreach (var milestone in milestones)
            {
                Console.WriteLine(milestone.ToString());
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching milestones: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("get", Description = "Get milestone by ID")]
    public async Task GetAsync([Argument(Description = "Milestone ID")] int id)
    {
        await EnsureAuthenticated();
        try
        {
            var milestone = await api.GetMilestoneAsync(id);
            Console.WriteLine($"Milestone Details:");
            Console.WriteLine(milestone.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching milestone: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("stats", Description = "Get milestone statistics")]
    public async Task StatsAsync([Argument(Description = "Milestone ID")] int id)
    {
        await EnsureAuthenticated();
        try
        {
            var stats = await api.GetMilestoneStatsAsync(id);
            Console.WriteLine($"Milestone Statistics (ID: {id}):");
            foreach (var stat in stats)
            {
                Console.WriteLine($"  {stat.Key}: {stat.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching milestone statistics: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("userstories", Description = "Get milestone user stories")]
    public async Task UserStoriesAsync([Argument(Description = "Milestone ID")] int id)
    {
        await EnsureAuthenticated();
        try
        {
            var stories = await api.GetMilestoneUserStoriesAsync(id);
            Console.WriteLine($"Milestone User Stories (ID: {id}):");
            foreach (var story in stories)
            {
                Console.WriteLine(story.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching user stories: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

