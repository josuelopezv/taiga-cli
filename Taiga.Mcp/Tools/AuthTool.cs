using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api.Models;
using Taiga.Api.Services;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class AuthTool(AuthService authService, TaigaApiFactory taigaApiFactory)
{
    [McpServerTool, Description("Authenticate with Taiga using username and password")]
    public async Task<string> LoginAsync(
        [Description("Username")] string username,
        [Description("Password")] string password,
        [Description("API Base URL (e.g., https://api.taiga.io/api/v1 or https://your-taiga-instance.com/api/v1)")] string? apiUrl = null)
    {
        try
        {
            var baseUrl = GetOrSetApiBaseUrl(apiUrl);
            // Create API client with the correct base URL
            var api = taigaApiFactory.Create(baseUrl, false);
            var response = await api.AuthenticateAsync(new AuthRequest
            {
                Username = username,
                Password = password
            });
            if (string.IsNullOrWhiteSpace(response.AuthToken))
                throw new InvalidOperationException("Received empty authentication token.");
            authService.SaveToken(response.AuthToken);
            return "Login successful!";
        }
        catch (Exception ex)
        {
            return $"Login failed: {ex.Message}";
        }
    }

    [McpServerTool, Description("Clear stored authentication token")]
    public string Logout()
    {
        authService.ClearToken();
        return "Logged out successfully.";
    }

    private string GetOrSetApiBaseUrl(string? apiUrl)
    {
        if (string.IsNullOrWhiteSpace(apiUrl))
            return authService.GetApiBaseUrl();
        // Ensure the URL ends with /api/v1 or add it if missing
        var normalizedUrl = NormalizeApiUrl(apiUrl.TrimEnd('/'));
        authService.SaveApiBaseUrl(normalizedUrl);
        return normalizedUrl;
    }

    private static string NormalizeApiUrl(string baseUrl) =>
        baseUrl.EndsWith("/api/v1", StringComparison.OrdinalIgnoreCase)
            ? baseUrl
            : baseUrl.EndsWith("/api", StringComparison.OrdinalIgnoreCase)
                ? $"{baseUrl}/v1"
                : $"{baseUrl}/api/v1";
}
