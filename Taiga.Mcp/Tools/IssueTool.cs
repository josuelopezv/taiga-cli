using Microsoft.Extensions.Logging;
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
            Logger.LogInformation("Listing issues for project {ProjectId}", project);
            await EnsureAuthenticated();
            var issues = await Api.GetIssuesAsync(project);
            Logger.LogDebug("Retrieved {Count} issues", issues.Count);

            if (issues.Count == 0)
            {
                Logger.LogInformation("No issues found for project {ProjectId}", project);
                return "No issues found.";
            }

            var result = $"Found {issues.Count} issue(s):\n\n";
            foreach (var issue in issues)
            {
                result += issue.ToString() + "\n\n";
            }
            Logger.LogInformation("Successfully listed {Count} issues", issues.Count);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching issues for project {ProjectId}", project);
            return $"Error fetching issues: {ex.Message}";
        }
    }

    [McpServerTool(Name = "GetIssue", ReadOnly = true, Destructive = false), Description("Get issue by ID")]
    public async Task<string> GetAsync([Description("Issue ID")] int id, [Description("Project ID to filter by")] int project)
    {
        try
        {
            Logger.LogInformation("Getting issue {IssueId} from project {ProjectId}", id, project);
            await EnsureAuthenticated();
            var issue = await Api.GetIssueAsync(id, project);
            Logger.LogInformation("Successfully retrieved issue {IssueId}", id);
            return $"Issue Details:\n{issue}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching issue {IssueId} from project {ProjectId}", id, project);
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
        [Description("Tags (comma-separated)")] string? tags = null,
        [Description("Assigned username")] string? assignedTo = null)
    {
        try
        {
            Logger.LogInformation("Creating new issue in project {ProjectId} with subject '{Subject}'", project, subject);
            await EnsureAuthenticated();
            var data = new Dictionary<string, object>
            {
                ["project"] = project,
                ["subject"] = subject
            };

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (!string.IsNullOrWhiteSpace(status))
            {
                Logger.LogDebug("Setting status for issue: {Status}", status);
                data["status"] = await GetStatusFromName(status, StatusType.IssueStatus, project);
            }

            if (!string.IsNullOrWhiteSpace(tags))
                data["tags"] = tags.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

            if (!string.IsNullOrWhiteSpace(type))
            {
                Logger.LogDebug("Setting type for issue: {Type}", type);
                data["type"] = await GetStatusFromName(type, StatusType.IssueType, project);
            }

            if (!string.IsNullOrWhiteSpace(priority))
            {
                Logger.LogDebug("Setting priority for issue: {Priority}", priority);
                data["priority"] = await GetStatusFromName(priority, StatusType.Priority, project);
            }

            if (!string.IsNullOrWhiteSpace(severity))
            {
                Logger.LogDebug("Setting severity for issue: {Severity}", severity);
                data["severity"] = await GetStatusFromName(severity, StatusType.Severity, project);
            }

            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                Logger.LogDebug("Assigning issue to user: {AssignedTo}", assignedTo);
                data["assigned_to"] = await GetUserIdFromUsername(assignedTo, project);
            }

            Logger.LogDebug("Creating issue with {FieldCount} fields", data.Count);
            var issue = await Api.CreateIssueAsync(data);
            Logger.LogInformation("Successfully created issue with ID {IssueId}", issue.Id);
            return $"Issue created successfully:\n{issue}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating issue in project {ProjectId}", project);
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
        [Description("Tags (comma-separated)")] string? tags = null,
        [Description("Assigned username")] string? assignedTo = null)
    {
        try
        {
            Logger.LogInformation("Editing issue {IssueId} in project {ProjectId}", refid, project);
            await EnsureAuthenticated();
            // Get the issue by ref to obtain its ID
            var issue = await Api.GetIssueAsync(refid, project);
            Logger.LogDebug("Retrieved issue {IssueId} for editing", issue.Id);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (!string.IsNullOrWhiteSpace(status))
            {
                Logger.LogDebug("Updating status for issue {IssueId}: {Status}", issue.Id, status);
                data["status"] = await GetStatusFromName(status, StatusType.IssueStatus, issue.Project);
            }

            if (!string.IsNullOrWhiteSpace(tags))
                data["tags"] = tags.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

            if (!string.IsNullOrWhiteSpace(type))
            {
                Logger.LogDebug("Updating type for issue {IssueId}: {Type}", issue.Id, type);
                data["type"] = await GetStatusFromName(type, StatusType.IssueType, issue.Project);
            }

            if (!string.IsNullOrWhiteSpace(priority))
            {
                Logger.LogDebug("Updating priority for issue {IssueId}: {Priority}", issue.Id, priority);
                data["priority"] = await GetStatusFromName(priority, StatusType.Priority, issue.Project);
            }

            if (!string.IsNullOrWhiteSpace(severity))
            {
                Logger.LogDebug("Updating severity for issue {IssueId}: {Severity}", issue.Id, severity);
                data["severity"] = await GetStatusFromName(severity, StatusType.Severity, issue.Project);
            }

            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                Logger.LogDebug("Reassigning issue {IssueId} to user: {AssignedTo}", issue.Id, assignedTo);
                data["assigned_to"] = await GetUserIdFromUsername(assignedTo, issue.Project);
            }

            if (data.Count == 0)
            {
                Logger.LogWarning("No fields specified for updating issue {IssueId}", issue.Id);
                return "No fields to update. Please specify at least one field to modify.";
            }

            Logger.LogDebug("Updating issue {IssueId} with {FieldCount} fields", issue.Id, data.Count);
            var updatedIssue = await Api.UpdateIssueAsync(issue.Id, data);
            Logger.LogInformation("Successfully updated issue {IssueId}", issue.Id);
            return $"Issue updated successfully:\n{updatedIssue}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error updating issue {IssueId} in project {ProjectId}", refid, project);
            return $"Error updating issue: {ex.Message}";
        }
    }

}
