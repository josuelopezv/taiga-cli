using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class UserTool(ITaigaApi api, IAuthService authService) : BaseTool(authService)
{
    [McpServerTool, Description("Get current user information")]
    public async Task<string> MeAsync()
    {
        try
        {
            await EnsureAuthenticated();
            var user = await api.GetCurrentUserAsync();
            return $"Current User:\n{user}";
        }
        catch (Exception ex)
        {
            return $"Error fetching user: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get user by ID")]
    public async Task<string> GetAsync([Description("User ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var user = await api.GetUserAsync(id);
            return $"User Details:\n{user}";
        }
        catch (Exception ex)
        {
            return $"Error fetching user: {ex.Message}";
        }
    }

    [McpServerTool, Description("List users (optionally filtered by project)")]
    public async Task<string> ListAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var users = await api.GetUsersAsync(project);

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

    [McpServerTool, Description("Get user statistics")]
    public async Task<string> StatsAsync([Description("User ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var stats = await api.GetUserStatsAsync(id);
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
