//#nullable disable
namespace TaigaCli.Models;

public record Points(
    [property: JsonPropertyName("1")] int? Ux,
    [property: JsonPropertyName("2")] int? Design,
    [property: JsonPropertyName("3")] int? Front,
    [property: JsonPropertyName("4")] int? Back
);

