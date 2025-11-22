using System.Net.Http.Headers;
using Taiga.Api.Services;

namespace Taiga.Api.Handlers;

public class AuthHeaderHandler(AuthService authService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = authService.GetToken();
        if (!string.IsNullOrWhiteSpace(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Headers.Add("x-disable-pagination", "1");
        var response = await base.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return response;
    }
}

