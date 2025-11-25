using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/webhooks")]
    Task<List<Webhook>> GetWebhooksAsync([Query] int? project = null);

    [Get("/webhooks/{id}")]
    Task<Webhook> GetWebhookAsync(int id);

    [Post("/webhooks")]
    Task<Webhook> CreateWebhookAsync([Body] object webhookData);

    [Patch("/webhooks/{id}")]
    Task<Webhook> UpdateWebhookAsync(int id, [Body] object webhookData);

    [Delete("/webhooks/{id}")]
    Task DeleteWebhookAsync(int id);

    [Post("/webhooks/{id}/test")]
    Task TestWebhookAsync(int id);

    [Get("/webhooks/{id}/logs")]
    Task<List<Dictionary<string, object>>> GetWebhookLogsAsync(int id);

}
