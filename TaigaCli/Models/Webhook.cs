using Newtonsoft.Json;

namespace TaigaCli.Models;

public class Webhook
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;
    
    [JsonProperty("key")]
    public string? Key { get; set; }
    
    [JsonProperty("project")]
    public int Project { get; set; }
    
    [JsonProperty("active")]
    public bool Active { get; set; }
}

