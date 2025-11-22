//#nullable disable
namespace Taiga.Api.Models;

public record WikiPage(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("content")] string? Content,
    [property: JsonPropertyName("project")] int Project
)
{
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"  ID: {Id}");
        sb.AppendLine($"  Slug: {Slug}");
        sb.AppendLine($"  Project: {Project}");
        if (!string.IsNullOrWhiteSpace(Content))
        {
            sb.AppendLine("  Content:");
            sb.AppendLine(Content);
        }
        return sb.ToString().TrimEnd();
    }
}

