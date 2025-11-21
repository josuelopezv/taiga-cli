//#nullable disable
namespace TaigaCli.Models;

public class ProjectRole
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("project")]
    public int Project { get; set; }

    [JsonPropertyName("computable")]
    public bool Computable { get; set; }
}

