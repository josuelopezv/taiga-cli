//#nullable disable
namespace Taiga.Api.Models;

public record ProjectRole(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("project")] int Project,
    [property: JsonPropertyName("computable")] bool Computable
)
{
    public override string ToString() => $"  ID: {Id}, Name: {Name}, Slug: {Slug}";
}

