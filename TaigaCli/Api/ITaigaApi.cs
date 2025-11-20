using Refit;
using TaigaCli.Models;

namespace TaigaCli.Api;

public interface ITaigaApi
{
    [Post("/auth")]
    Task<AuthResponse> AuthenticateAsync([Body] AuthRequest request);

    [Get("/users/me")]
    Task<User> GetCurrentUserAsync();

    [Get("/projects")]
    Task<List<Project>> GetProjectsAsync();

    [Get("/userstories")]
    Task<List<UserStory>> GetUserStoriesAsync([Query] int? project = null);
}

