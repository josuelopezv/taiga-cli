using Newtonsoft.Json;

namespace TaigaCli.Models;

public class Issue
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
    
    [JsonProperty("severity")]
    public int? Severity { get; set; }
    
    [JsonProperty("priority")]
    public int? Priority { get; set; }
    
    [JsonProperty("type")]
    public int? Type { get; set; }
    
    [JsonProperty("assigned_to")]
    public int? AssignedTo { get; set; }
}

