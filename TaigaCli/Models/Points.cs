//#nullable disable
namespace TaigaCli.Models;

public record Points(
    [property: JsonPropertyName("1")] int? _1,
    [property: JsonPropertyName("2")] int? _2,
    [property: JsonPropertyName("3")] int? _3,
    [property: JsonPropertyName("4")] int? _4
);

