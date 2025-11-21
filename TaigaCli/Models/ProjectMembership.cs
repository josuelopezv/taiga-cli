#nullable disable
namespace TaigaCli.Models;

public class ProjectMembership
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("user")]
    public int User { get; set; }

    [JsonPropertyName("project")]
    public int Project { get; set; }

    [JsonPropertyName("role")]
    public int Role { get; set; }

    [JsonPropertyName("is_admin")]
    public bool IsAdmin { get; set; }
}

