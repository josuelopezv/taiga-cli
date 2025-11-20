using Newtonsoft.Json;

namespace TaigaCli.Models;

public class Priority
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("color")]
    public string? Color { get; set; }
    
    [JsonProperty("project")]
    public int Project { get; set; }
    
    [JsonProperty("order")]
    public int Order { get; set; }
}

