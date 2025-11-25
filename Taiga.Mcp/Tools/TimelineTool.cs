using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class TimelineTool(ITaigaApi api, IAuthService authService) : BaseTool(authService)
{
    [McpServerTool, Description("Get project timeline")]
    public async Task<string> ProjectAsync([Description("Project ID")] int projectId)
    {
        try
        {
            await EnsureAuthenticated();
            var timeline = await api.GetProjectTimelineAsync(projectId);
            var result = $"Project Timeline (Project ID: {projectId}):\n";
            foreach (var entry in timeline)
            {
                result += $"  Entry {entry.Id}: {entry.CreatedAt} - Type: {entry.Type}\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching timeline: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get profile timeline")]
    public async Task<string> ProfileAsync([Description("Project ID")] int projectId)
    {
        try
        {
            await EnsureAuthenticated();
            var timeline = await api.GetProfileTimelineAsync(projectId);
            var result = $"Profile Timeline (Project ID: {projectId}):\n";
            foreach (var entry in timeline)
            {
                result += $"  Entry {entry.Id}: {entry.CreatedAt} - Type: {entry.Type}\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching timeline: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get user timeline")]
    public async Task<string> UserAsync(
        [Description("Project ID")] int projectId,
        [Description("User ID")] int userId)
    {
        try
        {
            await EnsureAuthenticated();
            var timeline = await api.GetUserTimelineAsync(projectId, userId);
            var result = $"User Timeline (Project ID: {projectId}, User ID: {userId}):\n";
            foreach (var entry in timeline)
            {
                result += $"  Entry {entry.Id}: {entry.CreatedAt} - Type: {entry.Type}\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching timeline: {ex.Message}";
        }
    }

}
