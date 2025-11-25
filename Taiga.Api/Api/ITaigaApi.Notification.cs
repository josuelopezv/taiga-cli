using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/notifications")]
    Task<List<Notification>> GetNotificationsAsync();

    [Get("/notifications/{id}")]
    Task<Notification> GetNotificationAsync(int id);

    [Patch("/notifications/{id}")]
    Task<Notification> MarkNotificationAsReadAsync(int id);

    [Patch("/notifications/read")]
    Task MarkAllNotificationsAsReadAsync();

    [Get("/notifications/unread")]
    Task<int> GetUnreadNotificationsCountAsync();
}
