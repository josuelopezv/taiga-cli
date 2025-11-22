//#nullable disable
namespace Taiga.Api.Models;

public record ProjectExtraInfo(
      [property: JsonPropertyName("name")] string Name,
      [property: JsonPropertyName("slug")] string Slug,
      [property: JsonPropertyName("logo_small_url")] string LogoSmallUrl,
      [property: JsonPropertyName("id")] int Id
  );

