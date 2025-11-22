//#nullable disable
namespace Taiga.Api.Models;

public record StatusExtraInfo(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("color")] string Color,
    [property: JsonPropertyName("is_closed")] bool IsClosed
);

