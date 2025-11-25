//#nullable disable
namespace Taiga.Api.Models;

public record Milestone(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("project")] int Project,
    [property: JsonPropertyName("estimated_start")] DateTime? EstimatedStart,
    [property: JsonPropertyName("estimated_finish")] DateTime? EstimatedFinish,
    [property: JsonPropertyName("closed")] bool Closed
)
{
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"  ID: {Id}");
        sb.AppendLine($"  Name: {Name}");
        sb.AppendLine($"  Slug: {Slug}");
        sb.AppendLine($"  Project: {Project}");
        sb.AppendLine($"  Closed: {Closed}");
        if (EstimatedStart.HasValue)
        {
            sb.AppendLine($"  Estimated Start: {EstimatedStart}");
        }
        if (EstimatedFinish.HasValue)
        {
            sb.AppendLine($"  Estimated Finish: {EstimatedFinish}");
        }
        return sb.ToString().TrimEnd();
    }
}

