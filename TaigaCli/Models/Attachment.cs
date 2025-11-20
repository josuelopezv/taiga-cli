using Newtonsoft.Json;

namespace TaigaCli.Models;

public class Attachment
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("size")]
    public long Size { get; set; }
    
    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;
    
    [JsonProperty("description")]
    public string? Description { get; set; }
    
    [JsonProperty("attached_file")]
    public string? AttachedFile { get; set; }
}

