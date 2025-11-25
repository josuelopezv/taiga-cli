global using Taiga.Api.Services;
using Taiga.Api;

namespace Taiga.Cli.Commands;

public abstract class BaseCommand(IAuthService authService, ITaigaApi api)
{
    protected readonly IAuthService authService = authService;
    protected readonly ITaigaApi api = api;
    protected async Task EnsureAuthenticated()
    {
        var token = await authService.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            var jwt = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().ReadJwtToken(token);
            if (jwt.ValidTo > DateTime.UtcNow)
                return;
            Console.WriteLine("Authentication token has expired. Please run 'auth login' again.");
            Environment.Exit(1);
        }
        Console.WriteLine("Please run 'auth login' first.");
        Environment.Exit(1);
    }
}

