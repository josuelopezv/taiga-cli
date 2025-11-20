using Newtonsoft.Json;

namespace TaigaCli.Models;

public class UserStory
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("subject")]
    public string Subject { get; set; } = string.Empty;
    
    [JsonProperty("description")]
    public string? Description { get; set; }
    
    [JsonProperty("project")]
    public int Project { get; set; }
    
    [JsonProperty("status")]
    public int Status { get; set; }
}

