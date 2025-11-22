using Cocona;
using Taiga.Api;
using Taiga.Cli.Configuration;
using Taiga.Api.Services;

namespace Taiga.Cli.Commands;

[SubCommand("project", Description = "Commands for managing projects")]
public class ProjectCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("list", Description = "List all projects")]
    public async Task ListAsync()
    {
        EnsureAuthenticated();
        try
        {
            var projects = await api.GetProjectsAsync();

            if (projects.Count == 0)
            {
                Console.WriteLine("No projects found.");
                return;
            }

            Console.WriteLine($"Found {projects.Count} project(s):\n");
            foreach (var project in projects)
            {
                Console.WriteLine(project.ToString());
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching projects: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("get", Description = "Get project by ID")]
    public async Task GetAsync([Argument(Description = "Project ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var project = await api.GetProjectAsync(id);
            Console.WriteLine($"Project Details:");
            Console.WriteLine(project.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching project: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("stats", Description = "Get project statistics")]
    public async Task StatsAsync([Argument(Description = "Project ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var stats = await api.GetProjectStatsAsync(id);
            Console.WriteLine($"Project Statistics (ID: {id}):");
            foreach (var stat in stats)
            {
                Console.WriteLine($"  {stat.Key}: {stat.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching project statistics: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
