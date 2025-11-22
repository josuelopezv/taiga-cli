//#nullable disable
namespace Taiga.Api.Models;

public record OwnerExtraInfo(
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("full_name_display")] string FullNameDisplay,
    [property: JsonPropertyName("photo")] string Photo,
    [property: JsonPropertyName("big_photo")] string BigPhoto,
    [property: JsonPropertyName("gravatar_id")] string GravatarId,
    [property: JsonPropertyName("is_active")] bool IsActive,
    [property: JsonPropertyName("id")] int Id
);

