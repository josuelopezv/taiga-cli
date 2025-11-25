using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class StatusTool(ITaigaApi api, IAuthService authService) : BaseTool(authService)
{
    [McpServerTool, Description("List severities")]
    public async Task<string> SeveritiesAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var severities = await api.GetSeveritiesAsync(project);
            var result = "Severities:\n";
            foreach (var severity in severities)
            {
                result += severity.ToString() + "\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching severities: {ex.Message}";
        }
    }

    [McpServerTool, Description("List priorities")]
    public async Task<string> PrioritiesAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var priorities = await api.GetPrioritiesAsync(project);
            var result = "Priorities:\n";
            foreach (var priority in priorities)
            {
                result += priority.ToString() + "\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching priorities: {ex.Message}";
        }
    }

    [McpServerTool, Description("List issue statuses")]
    public async Task<string> IssueStatusesAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var statuses = await api.GetIssueStatusesAsync(project);
            var result = "Issue Statuses:\n";
            foreach (var status in statuses)
            {
                result += status.ToString() + "\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching issue statuses: {ex.Message}";
        }
    }

    [McpServerTool, Description("List issue types")]
    public async Task<string> IssueTypesAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var types = await api.GetIssueTypesAsync(project);
            var result = "Issue Types:\n";
            foreach (var type in types)
            {
                result += type.ToString() + "\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching issue types: {ex.Message}";
        }
    }

    [McpServerTool, Description("List task statuses")]
    public async Task<string> TaskStatusesAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var statuses = await api.GetTaskStatusesAsync(project);
            var result = "Task Statuses:\n";
            foreach (var status in statuses)
            {
                result += status.ToString() + "\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching task statuses: {ex.Message}";
        }
    }

    [McpServerTool, Description("List user story statuses")]
    public async Task<string> UserStoryStatusesAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var statuses = await api.GetUserStoryStatusesAsync(project);
            var result = "User Story Statuses:\n";
            foreach (var status in statuses)
            {
                result += status.ToString() + "\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching user story statuses: {ex.Message}";
        }
    }

}
