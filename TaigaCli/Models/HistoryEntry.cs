using Newtonsoft.Json;

namespace TaigaCli.Models;

public class HistoryEntry
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("user")]
    public int? User { get; set; }
    
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonProperty("type")]
    public int Type { get; set; }
    
    [JsonProperty("comment")]
    public string? Comment { get; set; }
    
    [JsonProperty("diff")]
    public Dictionary<string, object>? Diff { get; set; }
}

