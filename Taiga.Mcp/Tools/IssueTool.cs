using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class IssueTool(IServiceProvider serviceProvider) : BaseTool(serviceProvider)
{
    [McpServerTool(Name = "ListIssues", ReadOnly = true, Destructive = false), Description("List issues (optionally filtered by project)")]
    public async Task<string> ListAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var issues = await Api.GetIssuesAsync(project);

            if (issues.Count == 0)
            {
                return "No issues found.";
            }

            var result = $"Found {issues.Count} issue(s):\n\n";
            foreach (var issue in issues)
            {
                result += issue.ToString() + "\n\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching issues: {ex.Message}";
        }
    }

    [McpServerTool(Name = "GetIssue", ReadOnly = true, Destructive = false), Description("Get issue by ID")]
    public async Task<string> GetAsync([Description("Issue ID")] int id, [Description("Project ID to filter by")] int project)
    {
        try
        {
            await EnsureAuthenticated();
            var issue = await Api.GetIssueAsync(id, project);
            return $"Issue Details:\n{issue}";
        }
        catch (Exception ex)
        {
            return $"Error fetching issue: {ex.Message}";
        }
    }

    [McpServerTool(Name = "CreateIssue"), Description("Create a new issue")]
    public async Task<string> CreateAsync(
        [Description("Project ID")] int project,
        [Description("Subject/title")] string subject,
        [Description("Description (support markdown)")] string? description = null,
        [Description("Status Name (e.g. \"New\", \"In Progress\")")] string? status = null,
        [Description("Issue type Name (e.g. \"Bug\", \"Feature\", \"Task\")")] string? type = null,
        [Description("Priority Name (e.g. \"Low\", \"Medium\", \"High\")")] string? priority = null,
        [Description("Severity Name (e.g. \"Minor\", \"Major\", \"Critical\")")] string? severity = null,
        [Description("Assigned username")] string? assignedTo = null)
    {
        try
        {
            await EnsureAuthenticated();
            var data = new Dictionary<string, object>
            {
                ["project"] = project,
                ["subject"] = subject
            };

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (!string.IsNullOrWhiteSpace(status))
                data["status"] = GetStatusFromName(status, StatusType.IssueStatus, project);

            if (!string.IsNullOrWhiteSpace(type))
                data["type"] = GetStatusFromName(type, StatusType.IssueType, project);

            if (!string.IsNullOrWhiteSpace(priority))
                data["priority"] = GetStatusFromName(priority, StatusType.Priority, project);

            if (!string.IsNullOrWhiteSpace(severity))
                data["severity"] = GetStatusFromName(severity, StatusType.Severity, project);

            if (!string.IsNullOrWhiteSpace(assignedTo))
                data["assigned_to"] = GetUserIdFromUsername(assignedTo, project);

            var issue = await Api.CreateIssueAsync(data);
            return $"Issue created successfully:\n{issue}";
        }
        catch (Exception ex)
        {
            return $"Error creating issue: {ex.Message}";
        }
    }

    [McpServerTool(Name = "EditIssue"), Description("Edit an issue by ID")]
    public async Task<string> EditAsync(
        [Description("Issue ID")] int refid,
        [Description("Project ID")] int project,
        [Description("Subject/title")] string? subject = null,
        [Description("Description (support markdown)")] string? description = null,
        [Description("Status Name (e.g. \"New\", \"In Progress\")")] string? status = null,
        [Description("Issue type Name (e.g. \"Bug\", \"Feature\", \"Task\")")] string? type = null,
        [Description("Priority Name (e.g. \"Low\", \"Medium\", \"High\")")] string? priority = null,
        [Description("Severity Name (e.g. \"Minor\", \"Major\", \"Critical\")")] string? severity = null,
        [Description("Assigned username")] string? assignedTo = null)
    {
        try
        {
            await EnsureAuthenticated();
            // Get the issue by ref to obtain its ID
            var issue = await Api.GetIssueAsync(refid, project);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (!string.IsNullOrWhiteSpace(status))
                data["status"] = GetStatusFromName(status, StatusType.IssueStatus, issue.Project);

            if (!string.IsNullOrWhiteSpace(type))
                data["type"] = GetStatusFromName(type, StatusType.IssueType, issue.Project);

            if (!string.IsNullOrWhiteSpace(priority))
                data["priority"] = GetStatusFromName(priority, StatusType.Priority, issue.Project);

            if (!string.IsNullOrWhiteSpace(severity))
                data["severity"] = GetStatusFromName(severity, StatusType.Severity, issue.Project);

            if (!string.IsNullOrWhiteSpace(assignedTo))
                data["assigned_to"] = GetUserIdFromUsername(assignedTo, issue.Project);

            if (data.Count == 0)
            {
                return "No fields to update. Please specify at least one field to modify.";
            }

            var updatedIssue = await Api.UpdateIssueAsync(issue.Id, data);
            return $"Issue updated successfully:\n{updatedIssue}";
        }
        catch (Exception ex)
        {
            return $"Error updating issue: {ex.Message}";
        }
    }

}
