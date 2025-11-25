using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class SearchTool(IServiceProvider serviceProvider) : BaseTool(serviceProvider)
{
    [McpServerTool(Name = "SearchProject", ReadOnly = true, Destructive = false), Description("Search within a project")]
    public async Task<string> ProjectAsync(
        [Description("Project ID")] int project,
        [Description("Search text")] string text)
    {
        try
        {
            Logger.LogInformation("Searching project {ProjectId} for text '{SearchText}'", project, text);
            await EnsureAuthenticated();
            var results = await Api.SearchProjectAsync(project, text);
            Logger.LogDebug("Found {Count} search results", results.Count);
            var result = $"{results.Count} Search Results in Project {project} for '{text}':\n";
            result += results.ToString();
            Logger.LogInformation("Successfully completed search in project {ProjectId}", project);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error searching project {ProjectId} for text '{SearchText}'", project, text);
            return $"Error performing search: {ex.Message}";
        }
    }

}
