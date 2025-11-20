using Newtonsoft.Json;

namespace TaigaCli.Models;

public class AuthRequest
{
    [JsonProperty("type")]
    public string Type { get; set; } = "normal";
    
    [JsonProperty("username")]
    public string Username { get; set; } = string.Empty;
    
    [JsonProperty("password")]
    public string Password { get; set; } = string.Empty;
}

