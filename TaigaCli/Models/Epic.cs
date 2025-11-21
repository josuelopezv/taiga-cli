//#nullable disable
namespace TaigaCli.Models;

public record Epic(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("subject")] string Subject,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("project")] int Project,
    [property: JsonPropertyName("status")] int Status,
    [property: JsonPropertyName("color")] string? Color
)
{
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"  ID: {Id}");
        sb.AppendLine($"  Subject: {Subject}");
        sb.AppendLine($"  Project: {Project}");
        sb.AppendLine($"  Status: {Status}");
        if (!string.IsNullOrWhiteSpace(Color))
        {
            sb.AppendLine($"  Color: {Color}");
        }
        if (!string.IsNullOrWhiteSpace(Description))
        {
            sb.AppendLine($"  Description: {Description}");
        }
        return sb.ToString().TrimEnd();
    }
}

