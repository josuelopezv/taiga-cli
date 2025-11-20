using Refit;
using TaigaCli.Api;

namespace TaigaCli.Services;

public class TaigaApiFactory(AuthService authService, IHttpClientFactory httpClientFactory)
{
    public const string AuthHttpClientName = "TaigaAuthClient";

    public ITaigaApi Create() => Create(authService.GetApiBaseUrl());

    public ITaigaApi Create(string url) => Create(url, includeAuth: true);

    public ITaigaApi Create(string url, bool includeAuth)
    {
        var httpClient = includeAuth
            ? httpClientFactory.CreateClient(AuthHttpClientName)
            : httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(url);
        return RestService.For<ITaigaApi>(httpClient);
    }
}

