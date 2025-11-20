

namespace TaigaCli.Models;

public class Project
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("slug")]
    public string Slug { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

