#nullable disable
namespace TaigaCli.Models;

public class CustomAttribute
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("project")]
    public int Project { get; set; }

    [JsonPropertyName("order")]
    public int Order { get; set; }
}

