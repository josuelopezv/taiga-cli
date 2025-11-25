using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class ProjectTool(IServiceProvider serviceProvider) : BaseTool(serviceProvider)
{
    [McpServerTool(Name = "ListProjects", ReadOnly = true, Destructive = false), Description("List all projects")]
    public async Task<string> ListAsync()
    {
        try
        {
            await EnsureAuthenticated();
            var projects = await Api.GetProjectsAsync();

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

    //[McpServerTool(Name = "GetProject", ReadOnly = true, Destructive = false), Description("Get project by ID")]
    //public async Task<string> GetAsync([Description("Project ID")] int id)
    //{
    //    try
    //    {
    //        await EnsureAuthenticated();
    //        var project = await api.GetProjectAsync(id);
    //        return $"Project Details:\n{project}";
    //    }
    //    catch (Exception ex)
    //    {
    //        return $"Error fetching project: {ex.Message}";
    //    }
    //}

    //[McpServerTool, Description("Get project statistics")]
    //public async Task<string> StatsAsync([Description("Project ID")] int id)
    //{
    //    try
    //    {
    //        await EnsureAuthenticated();
    //        var stats = await api.GetProjectStatsAsync(id);
    //        var result = $"Project Statistics (ID: {id}):\n";
    //        foreach (var stat in stats)
    //        {
    //            result += $"  {stat.Key}: {stat.Value}\n";
    //        }
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        return $"Error fetching project statistics: {ex.Message}";
    //    }
    //}

}
