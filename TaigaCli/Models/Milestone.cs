//#nullable disable
namespace TaigaCli.Models;

public class Milestone
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("project")]
    public int Project { get; set; }

    [JsonPropertyName("estimated_start")]
    public DateTime? EstimatedStart { get; set; }

    [JsonPropertyName("estimated_finish")]
    public DateTime? EstimatedFinish { get; set; }

    [JsonPropertyName("closed")]
    public bool Closed { get; set; }
}

