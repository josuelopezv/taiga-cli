using Cocona;
using Taiga.Api;
using Taiga.Cli.Configuration;

namespace Taiga.Cli.Commands;

[SubCommand("issue", Description = "Commands for managing issues")]
public class IssueCommands(ITaigaApi _api, IAuthService _authService) : BaseCommand(_authService, _api)
{
    [Command("list", Description = "List issues (optionally filtered by project)")]
    public async Task ListAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        await EnsureAuthenticated();
        try
        {
            var issues = await api.GetIssuesAsync(project);

            if (issues.Count == 0)
            {
                Console.WriteLine("No issues found.");
                return;
            }

            Console.WriteLine($"Found {issues.Count} issue(s):\n");
            foreach (var issue in issues)
            {
                Console.WriteLine(issue.ToString());
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching issues: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("get", Description = "Get issue by ID")]
    public async Task GetAsync([Argument(Description = "Issue ID")] int id, [Option('p', Description = "Project ID to filter by")] int project)
    {
        await EnsureAuthenticated();
        try
        {
            var issue = await api.GetIssueAsync(id, project);
            Console.WriteLine($"Issue Details:");
            Console.WriteLine(issue.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching issue: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("history", Description = "Get issue history")]
    public async Task HistoryAsync([Argument(Description = "Issue ID")] int id)
    {
        await EnsureAuthenticated();
        try
        {
            var history = await api.GetIssueHistoryAsync(id);
            Console.WriteLine($"Issue History (ID: {id}):");
            foreach (var entry in history)
            {
                Console.WriteLine(entry.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching history: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("comments", Description = "Get issue comments")]
    public async Task CommentsAsync([Argument(Description = "Issue ID")] int id)
    {
        await EnsureAuthenticated();
        try
        {
            var comments = await api.GetIssueCommentsAsync(id);
            Console.WriteLine($"Issue Comments (ID: {id}):");
            foreach (var comment in comments)
            {
                Console.WriteLine(comment.ToString());
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching comments: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("create", Description = "Create a new issue")]
    public async Task CreateAsync(
        [Option('p', Description = "Project ID")] int project,
        [Option('t', Description = "Subject/title")] string subject,
        [Option('d', Description = "Description (support markdown)")] string? description = null,
        [Option("status", shortNames: ['s'], Description = "Status ID")] int? status = null,
        [Option("type", shortNames: ['y'], Description = "Issue type ID")] int? type = null,
        [Option("priority", shortNames: ['r'], Description = "Priority ID")] int? priority = null,
        [Option("severity", shortNames: ['v'], Description = "Severity ID")] int? severity = null,
        [Option("assigned-to", shortNames: ['a'], Description = "Assigned user ID")] int? assignedTo = null)
    {
        await EnsureAuthenticated();
        try
        {
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
            Console.WriteLine("Issue created successfully:");
            Console.WriteLine(issue.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating issue: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("edit", Description = "Edit an issue by ID")]
    public async Task EditAsync(
        [Argument(Description = "Issue ID (e.g., 123)")] int refid,
        [Option('p', Description = "Project ID to filter by")] int? project = null,
        [Option('t', Description = "Subject/title")] string? subject = null,
        [Option('d', Description = "Description (support markdown)")] string? description = null,
        [Option("status", shortNames: ['s'], Description = "Status ID")] int? status = null,
        [Option("type", shortNames: ['y'], Description = "Issue type ID")] int? type = null,
        [Option("priority", shortNames: ['r'], Description = "Priority ID")] int? priority = null,
        [Option("severity", shortNames: ['v'], Description = "Severity ID")] int? severity = null,
        [Option("assigned-to", shortNames: ['a'], Description = "Assigned user ID")] int? assignedTo = null)
    {
        await EnsureAuthenticated();
        try
        {
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
                Console.WriteLine("No fields to update. Please specify at least one field to modify.");
                return;
            }

            var updatedIssue = await api.UpdateIssueAsync(issue.Id, data);
            Console.WriteLine("Issue updated successfully:");
            Console.WriteLine(updatedIssue.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating issue: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

