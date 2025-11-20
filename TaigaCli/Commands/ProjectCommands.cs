using Cocona;
using TaigaCli.Api;
using TaigaCli.Services;

namespace TaigaCli.Commands;

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
                Console.WriteLine($"  ID: {project.Id}");
                Console.WriteLine($"  Name: {project.Name}");
                Console.WriteLine($"  Slug: {project.Slug}");
                if (!string.IsNullOrWhiteSpace(project.Description))
                {
                    Console.WriteLine($"  Description: {project.Description}");
                }
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching projects: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

