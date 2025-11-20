using Cocona;
using TaigaCli.Api;
using TaigaCli.Services;

namespace TaigaCli.Commands;

public class TestCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("connectivity", Description = "Test connectivity and authentication with Taiga API")]
    public async Task ConnectivityAsync()
    {
        Console.WriteLine("Testing Taiga CLI connectivity...\n");

        // Test 1: Check authentication status
        Console.WriteLine("1. Checking authentication status...");
        if (!AuthService.IsAuthenticated())
        {
            Console.WriteLine("   ❌ Not authenticated. Please run 'auth login' first.");
            Environment.Exit(1);
        }
        Console.WriteLine("   ✓ Authenticated");

        // Test 2: Verify token by fetching current user
        Console.WriteLine("\n2. Verifying authentication token...");
        try
        {
            var user = await api.GetCurrentUserAsync();
            Console.WriteLine($"   ✓ Token valid");
            Console.WriteLine($"   User: {user.Username} ({user.FullName})");
            Console.WriteLine($"   Email: {user.Email}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"   ❌ Token verification failed: {ex.Message}");
            Environment.Exit(1);
        }

        // Test 3: Fetch projects
        Console.WriteLine("\n3. Fetching projects...");
        try
        {
            var projects = await api.GetProjectsAsync();
            Console.WriteLine($"   ✓ Successfully fetched {projects.Count} project(s)");
            if (projects.Count > 0)
            {
                Console.WriteLine($"   Sample project: {projects[0].Name} (ID: {projects[0].Id})");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"   ❌ Failed to fetch projects: {ex.Message}");
            Environment.Exit(1);
        }

        // Test 4: Fetch user stories
        Console.WriteLine("\n4. Fetching user stories...");
        try
        {
            var stories = await api.GetUserStoriesAsync();
            Console.WriteLine($"   ✓ Successfully fetched {stories.Count} user story/stories");
            if (stories.Count > 0)
            {
                Console.WriteLine($"   Sample story: {stories[0].Subject} (ID: {stories[0].Id})");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"   ❌ Failed to fetch user stories: {ex.Message}");
            Environment.Exit(1);
        }

        Console.WriteLine("\n✅ All connectivity tests passed!");
    }
}

