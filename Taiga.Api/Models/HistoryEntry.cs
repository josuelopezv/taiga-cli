//#nullable disable
namespace Taiga.Api.Models;

public record HistoryEntry(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("user")] int? User,
    [property: JsonPropertyName("created_at")] DateTime CreatedAt,
    [property: JsonPropertyName("type")] int Type,
    [property: JsonPropertyName("comment")] string? Comment,
    [property: JsonPropertyName("diff")] Dictionary<string, object>? Diff
)
{
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.Append($"  Entry {Id}: {CreatedAt} - Type: {Type}");
        if (!string.IsNullOrWhiteSpace(Comment))
        {
            sb.AppendLine();
            sb.Append($"    Comment: {Comment}");
        }
        return sb.ToString();
    }
}

