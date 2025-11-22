using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;
using Taiga.Api.Services;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class TimelineTool(ITaigaApi api, AuthService authService)
{
    [McpServerTool, Description("Get project timeline")]
    public async Task<string> ProjectAsync([Description("Project ID")] int projectId)
    {
        try
        {
            EnsureAuthenticated();
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
            EnsureAuthenticated();
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
            EnsureAuthenticated();
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

    private void EnsureAuthenticated()
    {
        if (!authService.IsAuthenticated())
            throw new InvalidOperationException("Please run auth login first.");
    }
}
