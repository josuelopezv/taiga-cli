using Refit;
using TaigaCli.Api;
using TaigaCli.Handlers;

namespace TaigaCli.Services;

public class TaigaApiFactory(AuthService authService)
{
    public ITaigaApi Create() => Create(authService.GetApiBaseUrl());

    public ITaigaApi Create(string url)
    {
        // Create a new handler instance for each API client
        var handler = new AuthHeaderHandler(authService)
        {
            InnerHandler = new HttpClientHandler()
        };
        
        return RestService.For<ITaigaApi>(new HttpClient(handler)
        {
            BaseAddress = new Uri(url)
        });
    }
}

