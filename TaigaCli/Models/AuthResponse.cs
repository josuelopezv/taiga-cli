//#nullable disable
namespace TaigaCli.Models;

public record AuthResponse(
    [property: JsonPropertyName("auth_token")] string AuthToken
);

