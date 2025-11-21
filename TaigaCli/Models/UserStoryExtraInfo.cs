//#nullable disable
namespace TaigaCli.Models;

public record UserStoryExtraInfo(
    [property: JsonPropertyName("epics")] IReadOnlyList<Epic> Epics,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("ref")] int Ref,
    [property: JsonPropertyName("subject")] string Subject
);