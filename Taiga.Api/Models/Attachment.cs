//#nullable disable
namespace Taiga.Api.Models;

public record Attachment(
    [property: JsonPropertyName("id")] int? Id,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("size")] long? Size,
    [property: JsonPropertyName("url")] string? Url,
    [property: JsonPropertyName("description")] string? Description,
    [property: JsonPropertyName("attached_file")] string? AttachedFile
)
{
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.Append($"  ID: {Id}, Name: {Name}, Size: {Size} bytes");
        if (!string.IsNullOrWhiteSpace(Url))
        {
            sb.AppendLine();
            sb.Append($"    URL: {Url}");
        }
        return sb.ToString();
    }
}

