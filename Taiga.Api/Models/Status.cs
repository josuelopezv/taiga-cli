//#nullable disable
namespace Taiga.Api.Models;

public record Status(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("color")] string? Color,
    [property: JsonPropertyName("is_closed")] bool IsClosed,
    [property: JsonPropertyName("project")] int Project,
    [property: JsonPropertyName("order")] int Order
);

