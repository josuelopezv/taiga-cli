using Refit;
using TaigaCli.Models;

namespace TaigaCli.Api;

public partial interface ITaigaApi
{
    // Authentication
    [Post("/auth")]
    Task<AuthResponse> AuthenticateAsync([Body] AuthRequest request);

    // References
    [Get("/references/{projectId}/{ref}")]
    Task<Dictionary<string, object>> GetReferenceAsync(int projectId, string @ref);
}
