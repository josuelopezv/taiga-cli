//#nullable disable
namespace TaigaCli.Models;

public record Severity(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("color")] string? Color,
    [property: JsonPropertyName("project")] int Project,
    [property: JsonPropertyName("order")] int Order
);

