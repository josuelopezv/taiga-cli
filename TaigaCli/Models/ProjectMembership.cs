using Newtonsoft.Json;

namespace TaigaCli.Models;

public class ProjectMembership
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("user")]
    public int User { get; set; }
    
    [JsonProperty("project")]
    public int Project { get; set; }
    
    [JsonProperty("role")]
    public int Role { get; set; }
    
    [JsonProperty("is_admin")]
    public bool IsAdmin { get; set; }
}

