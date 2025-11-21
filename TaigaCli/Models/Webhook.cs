//#nullable disable
namespace TaigaCli.Models;

public class Webhook
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("key")]
    public string? Key { get; set; }

    [JsonPropertyName("project")]
    public int Project { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }
}

