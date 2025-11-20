using Cocona;
using TaigaCli.Api;
using TaigaCli.Services;

namespace TaigaCli.Commands;

public class MilestoneCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("list", Description = "List milestones (optionally filtered by project)")]
    public async Task ListAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
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
                Console.WriteLine($"  ID: {milestone.Id}");
                Console.WriteLine($"  Name: {milestone.Name}");
                Console.WriteLine($"  Slug: {milestone.Slug}");
                Console.WriteLine($"  Project: {milestone.Project}");
                Console.WriteLine($"  Closed: {milestone.Closed}");
                if (milestone.EstimatedStart.HasValue)
                {
                    Console.WriteLine($"  Estimated Start: {milestone.EstimatedStart}");
                }
                if (milestone.EstimatedFinish.HasValue)
                {
                    Console.WriteLine($"  Estimated Finish: {milestone.EstimatedFinish}");
                }
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
        EnsureAuthenticated();
        try
        {
            var milestone = await api.GetMilestoneAsync(id);
            Console.WriteLine($"Milestone Details:");
            Console.WriteLine($"  ID: {milestone.Id}");
            Console.WriteLine($"  Name: {milestone.Name}");
            Console.WriteLine($"  Slug: {milestone.Slug}");
            Console.WriteLine($"  Project: {milestone.Project}");
            Console.WriteLine($"  Closed: {milestone.Closed}");
            if (milestone.EstimatedStart.HasValue)
            {
                Console.WriteLine($"  Estimated Start: {milestone.EstimatedStart}");
            }
            if (milestone.EstimatedFinish.HasValue)
            {
                Console.WriteLine($"  Estimated Finish: {milestone.EstimatedFinish}");
            }
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
        EnsureAuthenticated();
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
        EnsureAuthenticated();
        try
        {
            var stories = await api.GetMilestoneUserStoriesAsync(id);
            Console.WriteLine($"Milestone User Stories (ID: {id}):");
            foreach (var story in stories)
            {
                Console.WriteLine($"  ID: {story.Id}, Subject: {story.Subject}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching user stories: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

