using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/timeline/{projectId}")]
    Task<List<HistoryEntry>> GetProjectTimelineAsync(int projectId);

    [Get("/timeline/{projectId}/profile")]
    Task<List<HistoryEntry>> GetProfileTimelineAsync(int projectId);

    [Get("/timeline/{projectId}/user/{userId}")]
    Task<List<HistoryEntry>> GetUserTimelineAsync(int projectId, int userId);
}
