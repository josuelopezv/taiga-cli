using Taiga.Cli.Services;

namespace Taiga.Cli.Commands;

public abstract class BaseCommand(AuthService authService)
{
    protected readonly AuthService AuthService = authService;

    protected void EnsureAuthenticated()
    {
        if (AuthService.IsAuthenticated()) return;
        Console.WriteLine("Please run 'auth login' first.");
        Environment.Exit(1);
    }
}

