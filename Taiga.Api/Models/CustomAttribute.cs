//#nullable disable
namespace Taiga.Api.Models;

public record CustomAttribute(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("project")] int Project,
    [property: JsonPropertyName("order")] int Order
);

