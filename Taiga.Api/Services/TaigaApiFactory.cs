using Refit;
using System.Text.Json;
using System.Text.Json.Serialization;
using Taiga.Api;

namespace Taiga.Api.Services;

public class TaigaApiFactory(AuthService authService, IHttpClientFactory httpClientFactory)
{
    public const string AuthHttpClientName = "TaigaAuthClient";
    private static readonly RefitSettings _settings = new()
    {
        ContentSerializer = new JsonContentSerializer(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        }),
        DeserializationExceptionFactory = async (httpResponseMessage, ex) => throw ex
    };

    public ITaigaApi Create() => Create(authService.GetApiBaseUrl());

    public ITaigaApi Create(string url) => Create(url, includeAuth: true);

    public ITaigaApi Create(string url, bool includeAuth)
    {
        var httpClient = includeAuth
            ? httpClientFactory.CreateClient(AuthHttpClientName)
            : httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(url);
        return RestService.For<ITaigaApi>(httpClient, _settings);
    }
}

