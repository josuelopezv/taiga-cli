//#nullable disable
namespace Taiga.Api.Models;

public record IssueType(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("color")] string? Color,
    [property: JsonPropertyName("project")] int Project,
    [property: JsonPropertyName("order")] int Order
);

