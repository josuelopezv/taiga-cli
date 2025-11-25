using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class IssueTool(ITaigaApi api, IAuthService authService) : BaseTool(authService)
{
    [McpServerTool, Description("List issues (optionally filtered by project)")]
    public async Task<string> ListAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var issues = await api.GetIssuesAsync(project);

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

    [McpServerTool, Description("Get issue by ID")]
    public async Task<string> GetAsync([Description("Issue ID")] int id, [Description("Project ID to filter by")] int project)
    {
        try
        {
            await EnsureAuthenticated();
            var issue = await api.GetIssueAsync(id, project);
            return $"Issue Details:\n{issue}";
        }
        catch (Exception ex)
        {
            return $"Error fetching issue: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get issue history")]
    public async Task<string> HistoryAsync([Description("Issue ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var history = await api.GetIssueHistoryAsync(id);
            var result = $"Issue History (ID: {id}):\n";
            foreach (var entry in history)
            {
                result += entry.ToString() + "\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching history: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get issue comments")]
    public async Task<string> CommentsAsync([Description("Issue ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var comments = await api.GetIssueCommentsAsync(id);
            var result = $"Issue Comments (ID: {id}):\n";
            foreach (var comment in comments)
            {
                result += comment.ToString() + "\n\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching comments: {ex.Message}";
        }
    }

    [McpServerTool, Description("Create a new issue")]
    public async Task<string> CreateAsync(
        [Description("Project ID")] int project,
        [Description("Subject/title")] string subject,
        [Description("Description (support markdown)")] string? description = null,
        [Description("Status ID")] int? status = null,
        [Description("Issue type ID")] int? type = null,
        [Description("Priority ID")] int? priority = null,
        [Description("Severity ID")] int? severity = null,
        [Description("Assigned user ID")] int? assignedTo = null)
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

            if (status.HasValue)
                data["status"] = status.Value;

            if (type.HasValue)
                data["type"] = type.Value;

            if (priority.HasValue)
                data["priority"] = priority.Value;

            if (severity.HasValue)
                data["severity"] = severity.Value;

            if (assignedTo.HasValue)
                data["assigned_to"] = assignedTo.Value;

            var issue = await api.CreateIssueAsync(data);
            return $"Issue created successfully:\n{issue}";
        }
        catch (Exception ex)
        {
            return $"Error creating issue: {ex.Message}";
        }
    }

    [McpServerTool, Description("Edit an issue by ID")]
    public async Task<string> EditAsync(
        [Description("Issue ID")] int refid,
        [Description("Project ID to filter by")] int? project = null,
        [Description("Subject/title")] string? subject = null,
        [Description("Description (support markdown)")] string? description = null,
        [Description("Status ID")] int? status = null,
        [Description("Issue type ID")] int? type = null,
        [Description("Priority ID")] int? priority = null,
        [Description("Severity ID")] int? severity = null,
        [Description("Assigned user ID")] int? assignedTo = null)
    {
        try
        {
            await EnsureAuthenticated();
            // Get the issue by ref to obtain its ID
            var issue = await api.GetIssueAsync(refid, project);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (status.HasValue)
                data["status"] = status.Value;

            if (type.HasValue)
                data["type"] = type.Value;

            if (priority.HasValue)
                data["priority"] = priority.Value;

            if (severity.HasValue)
                data["severity"] = severity.Value;

            if (assignedTo.HasValue)
                data["assigned_to"] = assignedTo.Value;

            if (data.Count == 0)
            {
                return "No fields to update. Please specify at least one field to modify.";
            }

            var updatedIssue = await api.UpdateIssueAsync(issue.Id, data);
            return $"Issue updated successfully:\n{updatedIssue}";
        }
        catch (Exception ex)
        {
            return $"Error updating issue: {ex.Message}";
        }
    }

}
