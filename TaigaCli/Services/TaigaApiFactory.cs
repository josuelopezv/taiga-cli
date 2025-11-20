using Refit;
using TaigaCli.Api;
using TaigaCli.Handlers;

namespace TaigaCli.Services;

public class TaigaApiFactory(AuthService authService, AuthHeaderHandler authHeaderHandler)
{
    public ITaigaApi Create() => Create(authService.GetApiBaseUrl());

    public ITaigaApi Create(string url) =>
        RestService.For<ITaigaApi>(new HttpClient(authHeaderHandler)
        {
            BaseAddress = new Uri(url)
        });
}

