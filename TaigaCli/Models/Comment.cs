#nullable disable
namespace TaigaCli.Models;

public class Comment
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("comment")]
    public string CommentText { get; set; } = string.Empty;

    [JsonPropertyName("user")]
    public int User { get; set; }

    [JsonPropertyName("created_date")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("modified_date")]
    public DateTime? ModifiedDate { get; set; }
}

