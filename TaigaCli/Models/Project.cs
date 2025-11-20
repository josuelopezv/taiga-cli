using Newtonsoft.Json;

namespace TaigaCli.Models;

public class Project
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("slug")]
    public string Slug { get; set; } = string.Empty;
    
    [JsonProperty("description")]
    public string? Description { get; set; }
}

