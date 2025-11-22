//#nullable disable
namespace Taiga.Api.Models;

public record AuthRequest(
    [property: JsonPropertyName("type")] string Type = "normal",
    [property: JsonPropertyName("username")] string Username = "",
    [property: JsonPropertyName("password")] string Password = ""
);

