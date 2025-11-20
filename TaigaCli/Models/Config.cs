using Newtonsoft.Json;

namespace TaigaCli.Models;

public class Config
{
    [JsonProperty("auth_token")]
    public string? AuthToken { get; set; }
    
    [JsonProperty("api_base_url")]
    public string ApiBaseUrl { get; set; } = "https://api.taiga.io/api/v1";
}

