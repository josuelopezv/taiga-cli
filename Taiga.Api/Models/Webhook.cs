//#nullable disable
namespace Taiga.Api.Models;

public record Webhook(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("key")] string? Key,
    [property: JsonPropertyName("project")] int Project,
    [property: JsonPropertyName("active")] bool Active
);

