//#nullable disable
namespace Taiga.Api.Models;

public record UserStoriesCounts(
       [property: JsonPropertyName("progress")] double? Progress,
       [property: JsonPropertyName("total")] int? Total
   );