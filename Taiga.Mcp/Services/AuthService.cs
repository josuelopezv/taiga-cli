namespace Taiga.Mcp.Services;

public class AuthService(TaigaApiFactory taigaApiFactory) : IAuthService
{
    public string GetApiBaseUrl() =>
       NormalizeApiUrl(Environment.GetEnvironmentVariable("TAIGA_API_URL") ?? throw new InvalidOperationException("API URL not configured."));
    public async Task<string> GetTokenAsync()
    {
        var username = Environment.GetEnvironmentVariable("TAIGA_USERNAME");
        var password = Environment.GetEnvironmentVariable("TAIGA_PASSWORD");
        var api = taigaApiFactory.Create();
        var authResponse = await api.AuthenticateAsync(new Taiga.Api.Models.AuthRequest
        {
            Username = username ?? string.Empty,
            Password = password ?? string.Empty
        });
        return authResponse?.AuthToken ?? throw new InvalidOperationException("Failed to retrieve auth token.");
    }

    private static string NormalizeApiUrl(string baseUrl) =>
        baseUrl.EndsWith("/api/v1", StringComparison.OrdinalIgnoreCase)
            ? baseUrl
            : baseUrl.EndsWith("/api", StringComparison.OrdinalIgnoreCase)
                ? $"{baseUrl}/v1"
                : $"{baseUrl}/api/v1";

}