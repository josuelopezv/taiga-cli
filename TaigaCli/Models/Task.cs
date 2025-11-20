using Newtonsoft.Json;

namespace TaigaCli.Models;

public class TaigaTask
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("subject")]
    public string Subject { get; set; } = string.Empty;
    
    [JsonProperty("description")]
    public string? Description { get; set; }
    
    [JsonProperty("project")]
    public int Project { get; set; }
    
    [JsonProperty("status")]
    public int Status { get; set; }
    
    [JsonProperty("user_story")]
    public int? UserStory { get; set; }
    
    [JsonProperty("assigned_to")]
    public int? AssignedTo { get; set; }
}

