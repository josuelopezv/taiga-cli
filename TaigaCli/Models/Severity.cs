

namespace TaigaCli.Models;

public class Severity
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("color")]
    public string? Color { get; set; }
    
    [JsonPropertyName("project")]
    public int Project { get; set; }
    
    [JsonPropertyName("order")]
    public int Order { get; set; }
}

