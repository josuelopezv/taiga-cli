using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Taiga.Mcp.Tools;

//[McpServerToolType]
public class StatusTool(IServiceProvider serviceProvider) : BaseTool(serviceProvider)
{
    [McpServerTool(Name = "GetAvailableStatus", ReadOnly = true, Destructive = false), Description("Get available status options")]
    public async Task<string> GetAvailableStatus([Description("Project ID to filter by")] int projectId)
    {
        await EnsureAuthenticated();
        var result = "";
        result += "Available Severities:\n";
        result += string.Join(", ", await Api.GetSeveritiesAsync(projectId));
        result += "Available Priorities:\n";
        result += string.Join(", ", await Api.GetPrioritiesAsync(projectId));
        result += "Available Issue Statuses:\n";
        result += string.Join(", ", await Api.GetIssueStatusesAsync(projectId));
        result += "Available Issue Types:\n";
        result += string.Join(", ", await Api.GetIssueTypesAsync(projectId));
        result += "Available Task Statuses:\n";
        result += string.Join(", ", await Api.GetTaskStatusesAsync(projectId));
        result += "Available User Story Statuses:\n";
        result += string.Join(", ", await Api.GetUserStoryStatusesAsync(projectId));
        return result;
    }

    //[McpServerTool(Name = "ListSeverities", ReadOnly = true, Destructive = false), Description("List severities")]
    //public async Task<string> SeveritiesAsync([Description("Project ID to filter by")] int? project = null)
    //{
    //    try
    //    {
    //        await EnsureAuthenticated();
    //        var severities = await api.GetSeveritiesAsync(project);
    //        var result = "Severities:\n";
    //        foreach (var severity in severities)
    //        {
    //            result += severity.ToString() + "\n";
    //        }
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return $"Error fetching severities: {ex.Message}";
    //    }
    //}

    //[McpServerTool(Name = "ListPriorities", ReadOnly = true, Destructive = false), Description("List priorities")]
    //public async Task<string> PrioritiesAsync([Description("Project ID to filter by")] int? project = null)
    //{
    //    try
    //    {
    //        await EnsureAuthenticated();
    //        var priorities = await api.GetPrioritiesAsync(project);
    //        var result = "Priorities:\n";
    //        foreach (var priority in priorities)
    //        {
    //            result += priority.ToString() + "\n";
    //        }
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return $"Error fetching priorities: {ex.Message}";
    //    }
    //}

    //[McpServerTool(Name = "ListIssueStatuses", ReadOnly = true, Destructive = false), Description("List issue statuses")]
    //public async Task<string> IssueStatusesAsync([Description("Project ID to filter by")] int? project = null)
    //{
    //    try
    //    {
    //        await EnsureAuthenticated();
    //        var statuses = await api.GetIssueStatusesAsync(project);
    //        var result = "Issue Statuses:\n";
    //        foreach (var status in statuses)
    //        {
    //            result += status.ToString() + "\n";
    //        }
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return $"Error fetching issue statuses: {ex.Message}";
    //    }
    //}

    //[McpServerTool(Name = "ListIssueTypes", ReadOnly = true, Destructive = false), Description("List issue types")]
    //public async Task<string> IssueTypesAsync([Description("Project ID to filter by")] int? project = null)
    //{
    //    try
    //    {
    //        await EnsureAuthenticated();
    //        var types = await api.GetIssueTypesAsync(project);
    //        var result = "Issue Types:\n";
    //        foreach (var type in types)
    //        {
    //            result += type.ToString() + "\n";
    //        }
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return $"Error fetching issue types: {ex.Message}";
    //    }
    //}

    //[McpServerTool(Name = "ListTaskStatuses", ReadOnly = true, Destructive = false), Description("List task statuses")]
    //public async Task<string> TaskStatusesAsync([Description("Project ID to filter by")] int? project = null)
    //{
    //    try
    //    {
    //        await EnsureAuthenticated();
    //        var statuses = await api.GetTaskStatusesAsync(project);
    //        var result = "Task Statuses:\n";
    //        foreach (var status in statuses)
    //        {
    //            result += status.ToString() + "\n";
    //        }
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return $"Error fetching task statuses: {ex.Message}";
    //    }
    //}

    //[McpServerTool(Name = "ListUserStoryStatuses", ReadOnly = true, Destructive = false), Description("List user story statuses")]
    //public async Task<string> UserStoryStatusesAsync([Description("Project ID to filter by")] int? project = null)
    //{
    //    try
    //    {
    //        await EnsureAuthenticated();
    //        var statuses = await api.GetUserStoryStatusesAsync(project);
    //        var result = "User Story Statuses:\n";
    //        foreach (var status in statuses)
    //        {
    //            result += status.ToString() + "\n";
    //        }
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return $"Error fetching user story statuses: {ex.Message}";
    //    }
    //}

}
