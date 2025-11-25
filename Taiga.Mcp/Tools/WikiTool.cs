using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class WikiTool(ITaigaApi api, IAuthService authService) : BaseTool(authService)
{
    [McpServerTool, Description("List wiki pages for a project")]
    public async Task<string> ListAsync([Description("Project ID")] int project)
    {
        try
        {
            await EnsureAuthenticated();
            var pages = await api.GetWikiPagesAsync(project);

            if (pages.Count == 0)
            {
                return $"No wiki pages found for project {project}.";
            }

            var result = $"Found {pages.Count} wiki page(s):\n\n";
            foreach (var page in pages)
            {
                result += page.ToString() + "\n\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching wiki pages: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get wiki page by ID")]
    public async Task<string> GetAsync([Description("Wiki Page ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var page = await api.GetWikiPageAsync(id);
            return $"Wiki Page Details:\n{page}";
        }
        catch (Exception ex)
        {
            return $"Error fetching wiki page: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get wiki page history")]
    public async Task<string> HistoryAsync([Description("Wiki Page ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var history = await api.GetWikiHistoryAsync(id);
            var result = $"Wiki Page History (ID: {id}):\n";
            foreach (var entry in history)
            {
                result += entry.ToString() + "\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching history: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get wiki page comments")]
    public async Task<string> CommentsAsync([Description("Wiki Page ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var comments = await api.GetWikiCommentsAsync(id);
            var result = $"Wiki Page Comments (ID: {id}):\n";
            foreach (var comment in comments)
            {
                result += comment.ToString() + "\n\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching comments: {ex.Message}";
        }
    }

}
