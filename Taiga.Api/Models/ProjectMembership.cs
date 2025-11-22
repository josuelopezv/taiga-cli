//#nullable disable
namespace Taiga.Api.Models;

public record ProjectMembership(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("user")] int User,
    [property: JsonPropertyName("project")] int Project,
    [property: JsonPropertyName("role")] int Role,
    [property: JsonPropertyName("is_admin")] bool IsAdmin
)
{
    public override string ToString() => $"  ID: {Id}, User: {User}, Role: {Role}";
}

