using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class MilestoneTool(ITaigaApi api, IAuthService authService) : BaseTool(authService)
{
    [McpServerTool, Description("List milestones (optionally filtered by project)")]
    public async Task<string> ListAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var milestones = await api.GetMilestonesAsync(project);

            if (milestones.Count == 0)
            {
                return "No milestones found.";
            }

            var result = $"Found {milestones.Count} milestone(s):\n\n";
            foreach (var milestone in milestones)
            {
                result += milestone.ToString() + "\n\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching milestones: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get milestone by ID")]
    public async Task<string> GetAsync([Description("Milestone ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var milestone = await api.GetMilestoneAsync(id);
            return $"Milestone Details:\n{milestone}";
        }
        catch (Exception ex)
        {
            return $"Error fetching milestone: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get milestone statistics")]
    public async Task<string> StatsAsync([Description("Milestone ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var stats = await api.GetMilestoneStatsAsync(id);
            var result = $"Milestone Statistics (ID: {id}):\n";
            foreach (var stat in stats)
            {
                result += $"  {stat.Key}: {stat.Value}\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching milestone statistics: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get milestone user stories")]
    public async Task<string> UserStoriesAsync([Description("Milestone ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var stories = await api.GetMilestoneUserStoriesAsync(id);
            var result = $"Milestone User Stories (ID: {id}):\n";
            foreach (var story in stories)
            {
                result += story.ToString() + "\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching user stories: {ex.Message}";
        }
    }

}
