//#nullable disable
namespace Taiga.Api.Models;

public record EpicExtraInfo(
       [property: JsonPropertyName("id")] int Id,
       [property: JsonPropertyName("ref")] int Ref,
       [property: JsonPropertyName("subject")] string Subject,
       [property: JsonPropertyName("color")] string Color,
       [property: JsonPropertyName("project")] Project Project
   );