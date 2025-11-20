using TaigaCli.Services;

namespace TaigaCli.Commands;

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

