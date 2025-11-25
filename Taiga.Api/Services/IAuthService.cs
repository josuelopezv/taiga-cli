namespace Taiga.Api.Services;

public interface IAuthService
{
    string GetApiBaseUrl();
    Task<string> GetTokenAsync();
}