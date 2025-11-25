using Cocona;
using Taiga.Api;
using Taiga.Cli.Configuration;

namespace Taiga.Cli.Commands;

[SubCommand("webhook", Description = "Commands for managing webhooks")]
public class WebhookCommands(ITaigaApi _api, IAuthService _authService) : BaseCommand(_authService, _api)
{
    [Command("list", Description = "List webhooks (optionally filtered by project)")]
    public async Task ListAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        await EnsureAuthenticated();
        try
        {
            var webhooks = await api.GetWebhooksAsync(project);

            if (webhooks.Count == 0)
            {
                Console.WriteLine("No webhooks found.");
                return;
            }

            Console.WriteLine($"Found {webhooks.Count} webhook(s):\n");
            foreach (var webhook in webhooks)
            {
                Console.WriteLine($"  ID: {webhook.Id}");
                Console.WriteLine($"  Name: {webhook.Name}");
                Console.WriteLine($"  URL: {webhook.Url}");
                Console.WriteLine($"  Project: {webhook.Project}");
                Console.WriteLine($"  Active: {webhook.Active}");
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching webhooks: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("get", Description = "Get webhook by ID")]
    public async Task GetAsync([Argument(Description = "Webhook ID")] int id)
    {
        await EnsureAuthenticated();
        try
        {
            var webhook = await api.GetWebhookAsync(id);
            Console.WriteLine($"Webhook Details:");
            Console.WriteLine($"  ID: {webhook.Id}");
            Console.WriteLine($"  Name: {webhook.Name}");
            Console.WriteLine($"  URL: {webhook.Url}");
            Console.WriteLine($"  Project: {webhook.Project}");
            Console.WriteLine($"  Active: {webhook.Active}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching webhook: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("logs", Description = "Get webhook logs")]
    public async Task LogsAsync([Argument(Description = "Webhook ID")] int id)
    {
        await EnsureAuthenticated();
        try
        {
            var logs = await api.GetWebhookLogsAsync(id);
            Console.WriteLine($"Webhook Logs (ID: {id}):");
            foreach (var log in logs)
            {
                Console.WriteLine($"  Log Entry:");
                foreach (var entry in log)
                {
                    Console.WriteLine($"    {entry.Key}: {entry.Value}");
                }
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching webhook logs: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

