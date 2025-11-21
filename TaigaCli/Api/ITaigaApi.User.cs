using Refit;
using TaigaCli.Models;

namespace TaigaCli.Api;

public partial interface ITaigaApi
{
    [Get("/users/me")]
    Task<User> GetCurrentUserAsync();

    [Get("/users/{id}")]
    Task<User> GetUserAsync(int id);

    [Get("/users")]
    Task<List<User>> GetUsersAsync([Query] int? project = null);

    [Patch("/users/{id}")]
    Task<User> UpdateUserAsync(int id, [Body] object userData);

    [Get("/users/{id}/stats")]
    Task<Dictionary<string, object>> GetUserStatsAsync(int id);
}
