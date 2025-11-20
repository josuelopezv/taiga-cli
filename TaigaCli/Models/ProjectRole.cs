using Newtonsoft.Json;

namespace TaigaCli.Models;

public class ProjectRole
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("slug")]
    public string Slug { get; set; } = string.Empty;
    
    [JsonProperty("project")]
    public int Project { get; set; }
    
    [JsonProperty("computable")]
    public bool Computable { get; set; }
}

