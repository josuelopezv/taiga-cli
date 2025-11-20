using Newtonsoft.Json;

namespace TaigaCli.Models;

public class User
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("username")]
    public string Username { get; set; } = string.Empty;
    
    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;
    
    [JsonProperty("full_name")]
    public string FullName { get; set; } = string.Empty;
}

