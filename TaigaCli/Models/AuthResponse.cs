

namespace TaigaCli.Models;

public class AuthResponse
{
    [JsonPropertyName("auth_token")]
    public string AuthToken { get; set; } = string.Empty;
}

