using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Mcp.Tools;

namespace Taiga.Mcp.DisabledTools;

//[McpServerToolType]
public class UserTool(IServiceProvider serviceProvider) : BaseTool(serviceProvider)
{
    [McpServerTool(Name = "GetCurrentUser", ReadOnly = true, Destructive = false), Description("Get current user information")]
    public async Task<string> MeAsync()
    {
        try
        {
            Logger.LogInformation("Getting current user information");
            await EnsureAuthenticated();
            var user = await Api.GetCurrentUserAsync();
            Logger.LogInformation("Successfully retrieved current user information");
            return $"Current User:\n{user}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching current user information");
            return $"Error fetching user: {ex.Message}";
        }
    }

    [McpServerTool(Name = "GetUser", ReadOnly = true, Destructive = false), Description("Get user by ID")]
    public async Task<string> GetAsync([Description("User ID")] int id)
    {
        try
        {
            Logger.LogInformation("Getting user {UserId}", id);
            await EnsureAuthenticated();
            var user = await Api.GetUserAsync(id);
            Logger.LogInformation("Successfully retrieved user {UserId}", id);
            return $"User Details:\n{user}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching user {UserId}", id);
            return $"Error fetching user: {ex.Message}";
        }
    }

    [McpServerTool(Name = "ListUsers", ReadOnly = true, Destructive = false), Description("List users (optionally filtered by project)")]
    public async Task<string> ListAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            Logger.LogInformation("Listing users for project {ProjectId}", project);
            await EnsureAuthenticated();
            var users = await Api.GetUsersAsync(project);
            Logger.LogDebug("Retrieved {Count} users", users.Count);

            if (users.Count == 0)
            {
                Logger.LogInformation("No users found for project {ProjectId}", project);
                return "No users found.";
            }

            var result = $"Found {users.Count} user(s):\n\n";
            foreach (var user in users)
            {
                result += user.ToString() + "\n\n";
            }
            Logger.LogInformation("Successfully listed {Count} users", users.Count);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching users for project {ProjectId}", project);
            return $"Error fetching users: {ex.Message}";
        }
    }

    [McpServerTool(Name = "GetUserStats", ReadOnly = true, Destructive = false), Description("Get user statistics")]
    public async Task<string> StatsAsync([Description("User ID")] int id)
    {
        try
        {
            Logger.LogInformation("Getting statistics for user {UserId}", id);
            await EnsureAuthenticated();
            var stats = await Api.GetUserStatsAsync(id);
            Logger.LogDebug("Retrieved {Count} statistics for user {UserId}", stats.Count, id);
            var result = $"User Statistics (ID: {id}):\n";
            foreach (var stat in stats)
            {
                result += $"  {stat.Key}: {stat.Value}\n";
            }
            Logger.LogInformation("Successfully retrieved statistics for user {UserId}", id);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching user statistics for user {UserId}", id);
            return $"Error fetching user statistics: {ex.Message}";
        }
    }

}
