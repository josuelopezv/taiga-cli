//#nullable disable
namespace Taiga.Api.Models;

public record Notification(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("read")] bool Read,
    [property: JsonPropertyName("created")] DateTime Created,
    [property: JsonPropertyName("data")] Dictionary<string, object>? Data
);

