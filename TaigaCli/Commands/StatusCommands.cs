using Cocona;
using TaigaCli.Api;
using TaigaCli.Configuration;
using TaigaCli.Services;

namespace TaigaCli.Commands;

[SubCommand("status", Description = "Commands for managing statuses, severities, priorities, and types")]
public class StatusCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("severities", Description = "List severities")]
    public async Task SeveritiesAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
        try
        {
            var severities = await api.GetSeveritiesAsync(project);
            Console.WriteLine($"Severities:");
            foreach (var severity in severities)
            {
                Console.WriteLine(severity.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching severities: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("priorities", Description = "List priorities")]
    public async Task PrioritiesAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
        try
        {
            var priorities = await api.GetPrioritiesAsync(project);
            Console.WriteLine($"Priorities:");
            foreach (var priority in priorities)
            {
                Console.WriteLine(priority.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching priorities: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("issue-statuses", Description = "List issue statuses")]
    public async Task IssueStatusesAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
        try
        {
            var statuses = await api.GetIssueStatusesAsync(project);
            Console.WriteLine($"Issue Statuses:");
            foreach (var status in statuses)
            {
                Console.WriteLine(status.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching issue statuses: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("issue-types", Description = "List issue types")]
    public async Task IssueTypesAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
        try
        {
            var types = await api.GetIssueTypesAsync(project);
            Console.WriteLine($"Issue Types:");
            foreach (var type in types)
            {
                Console.WriteLine(type.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching issue types: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("task-statuses", Description = "List task statuses")]
    public async Task TaskStatusesAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
        try
        {
            var statuses = await api.GetTaskStatusesAsync(project);
            Console.WriteLine($"Task Statuses:");
            foreach (var status in statuses)
            {
                Console.WriteLine(status.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching task statuses: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("userstory-statuses", Description = "List user story statuses")]
    public async Task UserStoryStatusesAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
        try
        {
            var statuses = await api.GetUserStoryStatusesAsync(project);
            Console.WriteLine($"User Story Statuses:");
            foreach (var status in statuses)
            {
                Console.WriteLine(status.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching user story statuses: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

