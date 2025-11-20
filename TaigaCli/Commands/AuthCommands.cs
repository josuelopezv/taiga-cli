using Cocona;
using TaigaCli.Models;
using TaigaCli.Services;

namespace TaigaCli.Commands;

public class AuthCommands(AuthService authService, TaigaApiFactory taigaApiFactory)
{
    [Command("login", Description = "Authenticate with Taiga using username and password")]
    public async Task LoginAsync(
        [Option('u', Description = "Username")] string username,
        [Option('p', Description = "Password")] string password,
        [Option('a', Description = "API Base URL (e.g., https://api.taiga.io/api/v1 or https://your-taiga-instance.com/api/v1)")] string? apiUrl = null)
    {
        try
        {
            var baseUrl = GetOrSetApiBaseUrl(apiUrl);
            // Create API client with the correct base URL
            var api = taigaApiFactory.Create(baseUrl);
            var response = await api.AuthenticateAsync(new AuthRequest
            {
                Username = username,
                Password = password
            });
            authService.SaveToken(response.AuthToken);
            Console.WriteLine("Login successful!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login failed: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("logout", Description = "Clear stored authentication token")]
    public void LogoutAsync()
    {
        authService.ClearToken();
        Console.WriteLine("Logged out successfully.");
    }

    private string GetOrSetApiBaseUrl(string? apiUrl)
    {
        if (string.IsNullOrWhiteSpace(apiUrl))
            return authService.GetApiBaseUrl();
        // Ensure the URL ends with /api/v1 or add it if missing
        var normalizedUrl = NormalizeApiUrl(apiUrl.TrimEnd('/'));
        authService.SaveApiBaseUrl(normalizedUrl);
        Console.WriteLine($"Using API URL: {normalizedUrl}");
        return normalizedUrl;
    }

    private static string NormalizeApiUrl(string baseUrl) =>
        baseUrl.EndsWith("/api/v1", StringComparison.OrdinalIgnoreCase)
            ? baseUrl
            : baseUrl.EndsWith("/api", StringComparison.OrdinalIgnoreCase)
                ? $"{baseUrl}/v1"
                : $"{baseUrl}/api/v1";
}

