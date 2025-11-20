using Newtonsoft.Json;

namespace TaigaCli.Models;

public class Status
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("slug")]
    public string Slug { get; set; } = string.Empty;
    
    [JsonProperty("color")]
    public string? Color { get; set; }
    
    [JsonProperty("is_closed")]
    public bool IsClosed { get; set; }
    
    [JsonProperty("project")]
    public int Project { get; set; }
    
    [JsonProperty("order")]
    public int Order { get; set; }
}

