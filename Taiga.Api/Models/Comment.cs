//#nullable disable
namespace Taiga.Api.Models;

public record Comment(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("comment")] string CommentText,
    [property: JsonPropertyName("user")] int User,
    [property: JsonPropertyName("created_date")] DateTime CreatedDate,
    [property: JsonPropertyName("modified_date")] DateTime? ModifiedDate
)
{
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"  Comment {Id}: {CreatedDate}");
        sb.Append($"    {CommentText}");
        return sb.ToString();
    }
}

