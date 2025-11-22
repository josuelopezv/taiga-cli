using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using Taiga.Cli.Services;

namespace Taiga.Cli.Handlers;

public class AuthHeaderHandler(AuthService authService, ILogger<AuthHeaderHandler> logger) : DelegatingHandler
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

        if (!response.IsSuccessStatusCode)
            logger.LogDebug("Request: {Method} {Uri} - Response: {StatusCode} Body: {Body}",
                request.Method,
                request.RequestUri,
                response.StatusCode,
                await response.Content.ReadAsStringAsync(cancellationToken));

        return response;
    }
}

