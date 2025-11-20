#pragma warning disable CA1873 // Avoid potentially expensive logging
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using TaigaCli.Services;

namespace TaigaCli.Handlers;

public class AuthHeaderHandler(AuthService authService, ILogger<AuthHeaderHandler> logger) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = authService.GetToken();
        if (!string.IsNullOrWhiteSpace(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await base.SendAsync(request, cancellationToken);

        logger.LogDebug("Request: {Method} {Uri} - Response: {StatusCode} Body: {Body}",
            request.Method,
            request.RequestUri,
            response.StatusCode,
            await response.Content.ReadAsStringAsync(cancellationToken));

        return response;
    }
}

