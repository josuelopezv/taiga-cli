

namespace TaigaCli.Models;

public class AuthRequest
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = "normal";
    
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
    
    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}

