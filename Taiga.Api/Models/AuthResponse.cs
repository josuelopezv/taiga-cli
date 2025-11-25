//#nullable disable
namespace Taiga.Api.Models;

public record AuthResponse(
    [property: JsonPropertyName("auth_token")] string AuthToken
);

