using Newtonsoft.Json;

namespace TaigaCli.Models;

public class CustomAttribute
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("description")]
    public string? Description { get; set; }
    
    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonProperty("project")]
    public int Project { get; set; }
    
    [JsonProperty("order")]
    public int Order { get; set; }
}

