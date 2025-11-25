using Cocona;
using Taiga.Api.Models;
using Taiga.Cli.Configuration;
using Taiga.Cli.Services;

namespace Taiga.Cli.Commands;

[SubCommand("auth", Description = "Commands for authentication")]
public class AuthCommands(ConfigService configService, TaigaApiFactory taigaApiFactory)
{
    [Command("login", Description = "Authenticate with Taiga using username and password")]
    public async void LoginAsync(
        [Option('u', Description = "Username")] string username,
        [Option('p', Description = "Password")] string password,
        [Option('a', Description = "API Base URL (e.g., https://api.taiga.io/api/v1 or https://your-taiga-instance.com/api/v1)")] string? apiUrl = null)
    {
        try
        {
            var baseUrl = GetOrSetApiBaseUrl(apiUrl);
            Console.WriteLine("Using API URL: {0}", baseUrl);
            // Create API client with the correct base URL
            var api = taigaApiFactory.Create(baseUrl);
            var request = new AuthRequest
            {
                Username = username,
                Password = password
            };
            var response = await api.AuthenticateAsync(request);
            if (string.IsNullOrWhiteSpace(response.AuthToken))
                throw new InvalidOperationException("Received empty authentication token.");
            configService.SaveToken(request, response.AuthToken);
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
        configService.ClearToken();
        Console.WriteLine("Logged out successfully.");
    }

    private string GetOrSetApiBaseUrl(string? apiUrl)
    {
        if (string.IsNullOrWhiteSpace(apiUrl))
            return configService.GetApiBaseUrl();
        // Ensure the URL ends with /api/v1 or add it if missing
        var normalizedUrl = NormalizeApiUrl(apiUrl.TrimEnd('/'));
        configService.SaveApiBaseUrl(normalizedUrl);
        return normalizedUrl;
    }

    private static string NormalizeApiUrl(string baseUrl) =>
        baseUrl.EndsWith("/api/v1", StringComparison.OrdinalIgnoreCase)
            ? baseUrl
            : baseUrl.EndsWith("/api", StringComparison.OrdinalIgnoreCase)
                ? $"{baseUrl}/v1"
                : $"{baseUrl}/api/v1";
}

