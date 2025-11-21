using Refit;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;

namespace TaigaCli.Services;

public class JsonContentSerializer(JsonSerializerOptions jsonSerializerOptions) : IHttpContentSerializer
{
    public JsonContentSerializer()
        : this(GetDefaultJsonSerializerOptions()) { }

    public HttpContent ToHttpContent<T>(T item) => JsonContent.Create(item, options: jsonSerializerOptions);

    public async Task<T?> FromHttpContentAsync<T>(
        HttpContent content,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            return await content.ReadFromJsonAsync<T>(jsonSerializerOptions, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deserializing HTTP content: {e.Message} {await content.ReadAsStringAsync(cancellationToken)}");
            throw new JsonException("Failed to deserialize HTTP content", e);
        }
    }

    public string? GetFieldNameForProperty(PropertyInfo propertyInfo) => propertyInfo switch
    {
        null => throw new ArgumentNullException(nameof(propertyInfo)),
        _ => propertyInfo
        .GetCustomAttributes<JsonPropertyNameAttribute>(true)
        .Select(a => a.Name)
        .FirstOrDefault()
    };

    public static JsonSerializerOptions GetDefaultJsonSerializerOptions()
    {
        // Default to case insensitive property name matching as that's likely the behavior most users expect
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        jsonSerializerOptions.Converters.Add(new ObjectToInferredTypesConverter());
        jsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        );

        return jsonSerializerOptions;
    }
}