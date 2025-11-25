global using Taiga.Api.Services;

namespace Taiga.Mcp.Tools;

public abstract class BaseTool(IAuthService authService)
{
    protected readonly IAuthService AuthService = authService;

    protected async Task EnsureAuthenticated()
    {
        var token = await AuthService.GetTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
            throw new InvalidOperationException("Please run auth login first.");
    }
}