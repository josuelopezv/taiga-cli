namespace Taiga.Cli.Services;

public class AuthService(ConfigService configService) : IAuthService
{

    public Task<string> GetTokenAsync() => configService.GetTokenAsync();

    public string GetApiBaseUrl() => configService.GetApiBaseUrl();

}

