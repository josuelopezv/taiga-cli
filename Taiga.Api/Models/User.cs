//#nullable disable
namespace Taiga.Api.Models;

public record User(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("full_name")] string FullName
)
{
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"  ID: {Id}");
        sb.AppendLine($"  Username: {Username}");
        sb.AppendLine($"  Full Name: {FullName}");
        sb.AppendLine($"  Email: {Email}");
        return sb.ToString().TrimEnd();
    }
}

