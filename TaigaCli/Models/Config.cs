//#nullable disable
namespace TaigaCli.Models;

public class Config
{
    [JsonPropertyName("auth_token")]
    public string? AuthToken { get; set; }

    [JsonPropertyName("api_base_url")]
    public string ApiBaseUrl { get; set; } = "https://api.taiga.io/api/v1";
}

