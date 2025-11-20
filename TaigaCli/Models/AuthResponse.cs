using Newtonsoft.Json;

namespace TaigaCli.Models;

public class AuthResponse
{
    [JsonProperty("auth_token")]
    public string AuthToken { get; set; } = string.Empty;
}

