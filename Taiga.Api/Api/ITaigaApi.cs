using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

[Headers("Authorization: Bearer", "x-disable-pagination: 1")]
public partial interface ITaigaApi
{
    // Authentication
    [Headers("Authorization:")]
    [Post("/auth")]
    Task<AuthResponse> AuthenticateAsync([Body] AuthRequest request);

    // References
    [Get("/references/{projectId}/{ref}")]
    Task<Dictionary<string, object>> GetReferenceAsync(int projectId, string @ref);
}
