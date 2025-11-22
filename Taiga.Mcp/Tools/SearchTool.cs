using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;
using Taiga.Api.Services;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class SearchTool(ITaigaApi api, AuthService authService)
{
    [McpServerTool, Description("Search within a project")]
    public async Task<string> ProjectAsync(
        [Description("Project ID")] int project,
        [Description("Search text")] string text)
    {
        try
        {
            EnsureAuthenticated();
            var results = await api.SearchProjectAsync(project, text);
            var result = $"{results.Count} Search Results in Project {project} for '{text}':\n";
            result += results.ToString();
            return result;
        }
        catch (Exception ex)
        {
            return $"Error performing search: {ex.Message}";
        }
    }

    private void EnsureAuthenticated()
    {
        if (!authService.IsAuthenticated())
            throw new InvalidOperationException("Please run auth login first.");
    }
}
