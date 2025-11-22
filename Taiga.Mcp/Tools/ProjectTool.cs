using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;
using Taiga.Api.Services;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class ProjectTool(ITaigaApi api, AuthService authService)
{
    [McpServerTool, Description("List all projects")]
    public async Task<string> ListAsync()
    {
        try
        {
            EnsureAuthenticated();
            var projects = await api.GetProjectsAsync();

            if (projects.Count == 0)
            {
                return "No projects found.";
            }

            var result = $"Found {projects.Count} project(s):\n\n";
            foreach (var project in projects)
            {
                result += project.ToString() + "\n\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching projects: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get project by ID")]
    public async Task<string> GetAsync([Description("Project ID")] int id)
    {
        try
        {
            EnsureAuthenticated();
            var project = await api.GetProjectAsync(id);
            return $"Project Details:\n{project}";
        }
        catch (Exception ex)
        {
            return $"Error fetching project: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get project statistics")]
    public async Task<string> StatsAsync([Description("Project ID")] int id)
    {
        try
        {
            EnsureAuthenticated();
            var stats = await api.GetProjectStatsAsync(id);
            var result = $"Project Statistics (ID: {id}):\n";
            foreach (var stat in stats)
            {
                result += $"  {stat.Key}: {stat.Value}\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching project statistics: {ex.Message}";
        }
    }

    private void EnsureAuthenticated()
    {
        if (!authService.IsAuthenticated())
            throw new InvalidOperationException("Please run auth login first.");
    }
}
