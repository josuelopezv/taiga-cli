//#nullable disable
namespace Taiga.Api.Models;

public record Neighbors(
    [property: JsonPropertyName("previous")] Previous Previous,
    [property: JsonPropertyName("next")] object Next
);

public record Next(
     [property: JsonPropertyName("id")] int Id,
     [property: JsonPropertyName("ref")] int Ref,
     [property: JsonPropertyName("subject")] string Subject
 );

public record Previous(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("ref")] int Ref,
    [property: JsonPropertyName("subject")] string Subject
);