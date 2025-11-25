//#nullable disable
using System.Text.Json.Serialization;
using Taiga.Api.Models;

namespace Taiga.Cli.Configuration;

public class Config
{
    [JsonPropertyName("auth_request")]
    public AuthRequest? AuthRequest { get; set; }

    [JsonPropertyName("auth_token")]
    public string? AuthToken { get; set; }

    [JsonPropertyName("api_base_url")]
    public string ApiBaseUrl { get; set; } = "https://api.taiga.io/api/v1";
}

