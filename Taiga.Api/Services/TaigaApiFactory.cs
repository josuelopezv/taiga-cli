using Refit;
using System.Text.Json;

namespace Taiga.Api.Services;

public class TaigaApiFactory(IAuthService authService)
{
    public ITaigaApi Create() => Create(authService.GetApiBaseUrl());

    public ITaigaApi Create(string url) => CreateBasic(url, (httpRequest, c) => authService.GetTokenAsync());

    public static ITaigaApi CreateBasic(string url,
                                        Func<HttpRequestMessage, CancellationToken, Task<string>>? authorizationHeaderValueGetter = null) =>
        RestService.For<ITaigaApi>(url, new()
        {
            ContentSerializer = new JsonContentSerializer(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            }),
            DeserializationExceptionFactory = async (httpResponseMessage, ex) => throw ex,
            AuthorizationHeaderValueGetter = authorizationHeaderValueGetter,
            ExceptionFactory = async (httpResponseMessage) =>
                httpResponseMessage.IsSuccessStatusCode
                ? null
                : throw new Exception($"API request failed: {httpResponseMessage.ReasonPhrase} {await httpResponseMessage.Content.ReadAsStringAsync()}")
        });
}

