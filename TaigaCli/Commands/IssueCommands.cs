using Cocona;
using TaigaCli.Api;
using TaigaCli.Services;

namespace TaigaCli.Commands;

public class IssueCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("list", Description = "List issues (optionally filtered by project)")]
    public async Task ListAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
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
                Console.WriteLine($"  ID: {issue.Id}");
                Console.WriteLine($"  Subject: {issue.Subject}");
                Console.WriteLine($"  Project: {issue.Project}");
                Console.WriteLine($"  Status: {issue.Status}");
                if (issue.Severity.HasValue)
                {
                    Console.WriteLine($"  Severity: {issue.Severity}");
                }
                if (issue.Priority.HasValue)
                {
                    Console.WriteLine($"  Priority: {issue.Priority}");
                }
                if (!string.IsNullOrWhiteSpace(issue.Description))
                {
                    Console.WriteLine($"  Description: {issue.Description}");
                }
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
    public async Task GetAsync([Argument(Description = "Issue ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var issue = await api.GetIssueAsync(id);
            Console.WriteLine($"Issue Details:");
            Console.WriteLine($"  ID: {issue.Id}");
            Console.WriteLine($"  Subject: {issue.Subject}");
            Console.WriteLine($"  Project: {issue.Project}");
            Console.WriteLine($"  Status: {issue.Status}");
            if (issue.Severity.HasValue)
            {
                Console.WriteLine($"  Severity: {issue.Severity}");
            }
            if (issue.Priority.HasValue)
            {
                Console.WriteLine($"  Priority: {issue.Priority}");
            }
            if (!string.IsNullOrWhiteSpace(issue.Description))
            {
                Console.WriteLine($"  Description: {issue.Description}");
            }
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
        EnsureAuthenticated();
        try
        {
            var history = await api.GetIssueHistoryAsync(id);
            Console.WriteLine($"Issue History (ID: {id}):");
            foreach (var entry in history)
            {
                Console.WriteLine($"  Entry {entry.Id}: {entry.CreatedAt} - Type: {entry.Type}");
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
        EnsureAuthenticated();
        try
        {
            var comments = await api.GetIssueCommentsAsync(id);
            Console.WriteLine($"Issue Comments (ID: {id}):");
            foreach (var comment in comments)
            {
                Console.WriteLine($"  Comment {comment.Id}: {comment.CreatedDate}");
                Console.WriteLine($"    {comment.CommentText}");
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching comments: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("attachments", Description = "List issue attachments")]
    public async Task AttachmentsAsync([Argument(Description = "Issue ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var attachments = await api.GetIssueAttachmentsAsync(id);
            Console.WriteLine($"Issue Attachments (ID: {id}):");
            foreach (var attachment in attachments)
            {
                Console.WriteLine($"  ID: {attachment.Id}, Name: {attachment.Name}, Size: {attachment.Size} bytes");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching attachments: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

