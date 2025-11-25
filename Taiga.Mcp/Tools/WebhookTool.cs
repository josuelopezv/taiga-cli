using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class WebhookTool(ITaigaApi api, IAuthService authService) : BaseTool(authService)
{
    [McpServerTool, Description("List webhooks (optionally filtered by project)")]
    public async Task<string> ListAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var webhooks = await api.GetWebhooksAsync(project);

            if (webhooks.Count == 0)
            {
                return "No webhooks found.";
            }

            var result = $"Found {webhooks.Count} webhook(s):\n\n";
            foreach (var webhook in webhooks)
            {
                result += $"  ID: {webhook.Id}\n";
                result += $"  Name: {webhook.Name}\n";
                result += $"  URL: {webhook.Url}\n";
                result += $"  Project: {webhook.Project}\n";
                result += $"  Active: {webhook.Active}\n\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching webhooks: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get webhook by ID")]
    public async Task<string> GetAsync([Description("Webhook ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var webhook = await api.GetWebhookAsync(id);
            return $"Webhook Details:\n  ID: {webhook.Id}\n  Name: {webhook.Name}\n  URL: {webhook.Url}\n  Project: {webhook.Project}\n  Active: {webhook.Active}";
        }
        catch (Exception ex)
        {
            return $"Error fetching webhook: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get webhook logs")]
    public async Task<string> LogsAsync([Description("Webhook ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var logs = await api.GetWebhookLogsAsync(id);
            var result = $"Webhook Logs (ID: {id}):\n";
            foreach (var log in logs)
            {
                result += "  Log Entry:\n";
                foreach (var entry in log)
                {
                    result += $"    {entry.Key}: {entry.Value}\n";
                }
                result += "\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching webhook logs: {ex.Message}";
        }
    }

}
