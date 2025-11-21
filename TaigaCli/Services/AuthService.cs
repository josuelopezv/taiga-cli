
using System.Text.Json;
using TaigaCli.Models;

namespace TaigaCli.Services;

public class AuthService
{
    private readonly string _configDirectory;
    private readonly string _configFilePath;
    private Config? _config;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true
    };

    public AuthService()
    {
        var homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        _configDirectory = Path.Combine(homeDirectory, ".taiga-cli");
        _configFilePath = Path.Combine(_configDirectory, "config.json");
        LoadConfig();
    }

    private void LoadConfig()
    {
        if (File.Exists(_configFilePath))
        {
            var json = File.ReadAllText(_configFilePath);
            _config = JsonSerializer.Deserialize<Config>(json) ?? new Config();
        }
        else
            _config = new Config();
    }

    private void SaveConfig(bool showMessage = true)
    {
        if (!Directory.Exists(_configDirectory))
            Directory.CreateDirectory(_configDirectory);
        File.WriteAllText(_configFilePath, JsonSerializer.Serialize(_config, _jsonOptions));
        if (showMessage)
            Console.WriteLine($"Config saved to {_configFilePath}");
    }

    public void SaveToken(string token)
    {
        _config ??= new Config();
        _config.AuthToken = token;
        SaveConfig();
    }

    public string? GetToken() => _config?.AuthToken;

    public void ClearToken()
    {
        _config ??= new Config();
        _config.AuthToken = null;
        SaveConfig();
    }

    public bool IsAuthenticated() => !string.IsNullOrWhiteSpace(GetToken());

    public void SaveApiBaseUrl(string apiBaseUrl)
    {
        _config ??= new Config();
        _config.ApiBaseUrl = apiBaseUrl;
        SaveConfig(false);
    }

    public string GetApiBaseUrl() => _config?.ApiBaseUrl ?? "https://api.taiga.io/api/v1";
}

