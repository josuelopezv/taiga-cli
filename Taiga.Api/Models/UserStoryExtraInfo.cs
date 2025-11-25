//#nullable disable
namespace Taiga.Api.Models;

public record UserStoryExtraInfo(
       [property: JsonPropertyName("id")] int Id,
       [property: JsonPropertyName("ref")] int Ref,
       [property: JsonPropertyName("subject")] string Subject,
       [property: JsonPropertyName("epics")] IReadOnlyList<EpicExtraInfo> Epics
)
{
    public override string ToString() => $"  ID: {Ref}, Subject: {Subject}";
}