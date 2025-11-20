

namespace TaigaCli.Models;

public class UserStory
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("subject")]
    public string Subject { get; set; } = string.Empty;
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("project")]
    public int Project { get; set; }
    
    [JsonPropertyName("status")]
    public int Status { get; set; }
}

