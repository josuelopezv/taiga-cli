using System.Text.Json;
using Taiga.Api.Models;
using Taiga.Cli.Configuration;

namespace Taiga.Cli.Services;

public class ConfigService
{
    private readonly string _configDirectory;
    private readonly string _configFilePath;
    private Config? _config;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true
    };

    public ConfigService()
    {
        var homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        _configDirectory = Path.Combine(homeDirectory, ".taiga");
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

    public async Task<string> GetTokenAsync() => _config?.AuthToken ?? string.Empty;

    public void SaveApiBaseUrl(string apiBaseUrl)
    {
        _config ??= new Config();
        _config.ApiBaseUrl = apiBaseUrl;
        SaveConfig(false);
    }

    public string GetApiBaseUrl() => _config?.ApiBaseUrl ?? "https://api.taiga.io/api/v1";

    internal void SaveToken(AuthRequest request, string authToken)
    {
        _config ??= new Config();
        _config.AuthToken = authToken;
        _config.AuthRequest = request;
        SaveConfig(true);
    }

    internal void ClearToken()
    {
        _config?.AuthToken = string.Empty;
        SaveConfig(true);
    }
}

