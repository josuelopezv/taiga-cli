using Cocona;
using TaigaCli.Api;
using TaigaCli.Services;

namespace TaigaCli.Commands;

public class NotificationCommands(ITaigaApi api, AuthService authService) : BaseCommand(authService)
{
    [Command("list", Description = "List notifications")]
    public async Task ListAsync()
    {
        EnsureAuthenticated();
        try
        {
            var notifications = await api.GetNotificationsAsync();

            if (notifications.Count == 0)
            {
                Console.WriteLine("No notifications found.");
                return;
            }

            Console.WriteLine($"Found {notifications.Count} notification(s):\n");
            foreach (var notification in notifications)
            {
                Console.WriteLine($"  ID: {notification.Id}");
                Console.WriteLine($"  Read: {notification.Read}");
                Console.WriteLine($"  Created: {notification.Created}");
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching notifications: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("get", Description = "Get notification by ID")]
    public async Task GetAsync([Argument(Description = "Notification ID")] int id)
    {
        EnsureAuthenticated();
        try
        {
            var notification = await api.GetNotificationAsync(id);
            Console.WriteLine($"Notification Details:");
            Console.WriteLine($"  ID: {notification.Id}");
            Console.WriteLine($"  Read: {notification.Read}");
            Console.WriteLine($"  Created: {notification.Created}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching notification: {ex.Message}");
            Environment.Exit(1);
        }
    }

    [Command("unread", Description = "Get unread notifications count")]
    public async Task UnreadAsync()
    {
        EnsureAuthenticated();
        try
        {
            var count = await api.GetUnreadNotificationsCountAsync();
            Console.WriteLine($"Unread notifications: {count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching unread count: {ex.Message}");
            Environment.Exit(1);
        }
    }
}

