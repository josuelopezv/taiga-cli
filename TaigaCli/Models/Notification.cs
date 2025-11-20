using Newtonsoft.Json;

namespace TaigaCli.Models;

public class Notification
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("read")]
    public bool Read { get; set; }
    
    [JsonProperty("created")]
    public DateTime Created { get; set; }
    
    [JsonProperty("data")]
    public Dictionary<string, object>? Data { get; set; }
}

