using Cocona;
using Taiga.Api;
using Taiga.Cli.Configuration;
using Taiga.Api.Services;

namespace Taiga.Cli.Commands;

[SubCommand("user", Description = "Commands for managing users")]
public class UserCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("me", Description = "Get current user information")]
    public async Task MeAsync()
    {
        EnsureAuthenticated();
        try
        {
            var user = await api.GetCurrentUserAsync();
            Console.WriteLine($"Current User:");
            Console.WriteLine(user.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching user: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("get", Description = "Get user by ID")]
    public async Task GetAsync([Argument(Description = "User ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var user = await api.GetUserAsync(id);
            Console.WriteLine($"User Details:");
            Console.WriteLine(user.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching user: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("list", Description = "List users (optionally filtered by project)")]
    public async Task ListAsync([Option('p', Description = "Project ID to filter by")] int? project = null)
    {
        EnsureAuthenticated();
        try
        {
            var users = await api.GetUsersAsync(project);

            if (users.Count == 0)
            {
                Console.WriteLine("No users found.");
                return;
            }

            Console.WriteLine($"Found {users.Count} user(s):\n");
            foreach (var user in users)
            {
                Console.WriteLine(user.ToString());
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching users: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("stats", Description = "Get user statistics")]
    public async Task StatsAsync([Argument(Description = "User ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var stats = await api.GetUserStatsAsync(id);
            Console.WriteLine($"User Statistics (ID: {id}):");
            foreach (var stat in stats)
            {
                Console.WriteLine($"  {stat.Key}: {stat.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching user statistics: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

