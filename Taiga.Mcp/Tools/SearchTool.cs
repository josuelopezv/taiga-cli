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
            await EnsureAuthenticated();
            var results = await Api.SearchProjectAsync(project, text);
            var result = $"{results.Count} Search Results in Project {project} for '{text}':\n";
            result += results.ToString();
            return result;
        }
        catch (Exception ex)
        {
            return $"Error performing search: {ex.Message}";
        }
    }

}
