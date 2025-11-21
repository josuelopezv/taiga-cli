using Refit;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using TaigaCli.Models;

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
            return typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(PagedResult<>)
                ? await GetPagedResult<T>(content, cancellationToken)
                : await content.ReadFromJsonAsync<T>(jsonSerializerOptions, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deserializing HTTP content: {e.Message} {await content.ReadAsStringAsync(cancellationToken)}");
            throw new JsonException("Failed to deserialize HTTP content", e);
        }
    }

    private async Task<T> GetPagedResult<T>(HttpContent content, CancellationToken cancellationToken)
    {
        foreach (var header in content.Headers)
        {
            Console.WriteLine($"Header: {header.Key} = {string.Join(", ", header.Value)}");
        }

        Type type = typeof(T); // PagedResult<>
        Type innerType = type.GetGenericArguments()[0];
        object? items = await content.ReadFromJsonAsync(innerType, jsonSerializerOptions, cancellationToken);
        var result = Activator.CreateInstance<T>();
        void SetHeaderValue(string headerName, string propertyName)
        {
            if (!content.Headers.TryGetValues(headerName, out var values))
                return;
            if (!long.TryParse(values.FirstOrDefault(), out var parsedValue))
                throw new InvalidOperationException($"Header '{headerName}' value '{values.FirstOrDefault()}' is not a valid number.");
            var prop = type.GetProperty(propertyName);
            if (prop == null || !prop.CanWrite)
                throw new InvalidOperationException($"Property '{propertyName}' not found or not writable on type '{type.FullName}'.");
            prop.SetValue(result, Convert.ChangeType(parsedValue, prop.PropertyType));
        }
        SetHeaderValue("x-pagination-page", "Page");
        SetHeaderValue("x-pagination-page-size", "PageSize");
        SetHeaderValue("x-pagination-total", "Total");
        var itemsProperty = type.GetProperty("Items");
        if (itemsProperty == null || !itemsProperty.CanWrite)
            throw new InvalidOperationException($"Property 'Items' not found or not writable on type '{type.FullName}'.");
        itemsProperty.SetValue(result, items);
        return result;
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