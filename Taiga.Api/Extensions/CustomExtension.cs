namespace Taiga.Api.Extensions;

internal static class CustomExtension
{
    public static string JoinTags(this IReadOnlyList<List<string>> source, string? separator) =>
        string.Join(separator, source.Select(t => t[0]));

    public static string JoinToString(this IEnumerable<string?> source, string? separator) =>
       string.Join(separator, source);
}
