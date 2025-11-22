namespace Taiga.Api.Models;

public record TaskExtraInfo(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("ref")] int Ref,
    [property: JsonPropertyName("subject")] string Subject
)
{
    public override string ToString() => $"    #[{Ref}] {Subject}";
};
