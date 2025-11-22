using Microsoft.Extensions.Logging;
using Refit;
using System.Text.Json;
using TaigaCli.Api;

namespace TaigaCli.Services;

public class TaigaApiFactory(AuthService authService, IHttpClientFactory httpClientFactory, ILogger<TaigaApiFactory> logger)
{
    public const string AuthHttpClientName = "TaigaAuthClient";
    private static readonly RefitSettings _settings = new()
    {
        ContentSerializer = new JsonContentSerializer(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        }),
    };

    public ITaigaApi Create() => Create(authService.GetApiBaseUrl());

    public ITaigaApi Create(string url) => Create(url, includeAuth: true);

    public ITaigaApi Create(string url, bool includeAuth)
    {
        var httpClient = includeAuth
            ? httpClientFactory.CreateClient(AuthHttpClientName)
            : httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(url);
        return RestService.For<ITaigaApi>(httpClient, new RefitSettings()
        {
            ContentSerializer = new JsonContentSerializer(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            }),
            DeserializationExceptionFactory = async (httpResponseMessage, ex) =>
            {
                logger.LogError("Error deserializing response from {Url}: {StatusCode} - {ReasonPhrase}",
                    httpResponseMessage.RequestMessage?.RequestUri,
                    httpResponseMessage.StatusCode,
                    httpResponseMessage.ReasonPhrase);
                throw ex;
            }
        });
    }
}

