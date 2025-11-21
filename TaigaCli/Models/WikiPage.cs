//#nullable disable
namespace TaigaCli.Models;

public class WikiPage
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string? Content { get; set; }

    [JsonPropertyName("project")]
    public int Project { get; set; }
}

