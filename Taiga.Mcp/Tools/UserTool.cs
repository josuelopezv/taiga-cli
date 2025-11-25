using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

//[McpServerToolType]
public class UserTool(IServiceProvider serviceProvider) : BaseTool(serviceProvider)
{
    [McpServerTool(Name = "GetCurrentUser", ReadOnly = true, Destructive = false), Description("Get current user information")]
    public async Task<string> MeAsync()
    {
        try
        {
            await EnsureAuthenticated();
            var user = await Api.GetCurrentUserAsync();
            return $"Current User:\n{user}";
        }
        catch (Exception ex)
        {
            return $"Error fetching user: {ex.Message}";
        }
    }

    [McpServerTool(Name = "GetUser", ReadOnly = true, Destructive = false), Description("Get user by ID")]
    public async Task<string> GetAsync([Description("User ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var user = await Api.GetUserAsync(id);
            return $"User Details:\n{user}";
        }
        catch (Exception ex)
        {
            return $"Error fetching user: {ex.Message}";
        }
    }

    [McpServerTool(Name = "ListUsers", ReadOnly = true, Destructive = false), Description("List users (optionally filtered by project)")]
    public async Task<string> ListAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var users = await Api.GetUsersAsync(project);

            if (users.Count == 0)
            {
                return "No users found.";
            }

            var result = $"Found {users.Count} user(s):\n\n";
            foreach (var user in users)
            {
                result += user.ToString() + "\n\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching users: {ex.Message}";
        }
    }

    [McpServerTool(Name = "GetUserStats", ReadOnly = true, Destructive = false), Description("Get user statistics")]
    public async Task<string> StatsAsync([Description("User ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var stats = await Api.GetUserStatsAsync(id);
            var result = $"User Statistics (ID: {id}):\n";
            foreach (var stat in stats)
            {
                result += $"  {stat.Key}: {stat.Value}\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching user statistics: {ex.Message}";
        }
    }

}
