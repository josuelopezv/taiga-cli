using Newtonsoft.Json;

namespace TaigaCli.Models;

public class Milestone
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("slug")]
    public string Slug { get; set; } = string.Empty;
    
    [JsonProperty("description")]
    public string? Description { get; set; }
    
    [JsonProperty("project")]
    public int Project { get; set; }
    
    [JsonProperty("estimated_start")]
    public DateTime? EstimatedStart { get; set; }
    
    [JsonProperty("estimated_finish")]
    public DateTime? EstimatedFinish { get; set; }
    
    [JsonProperty("closed")]
    public bool Closed { get; set; }
}

