using Newtonsoft.Json;

namespace TaigaCli.Models;

public class WikiPage
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("slug")]
    public string Slug { get; set; } = string.Empty;
    
    [JsonProperty("content")]
    public string? Content { get; set; }
    
    [JsonProperty("project")]
    public int Project { get; set; }
}

