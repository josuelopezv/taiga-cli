using Refit;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/stats/discover")]
    Task<Dictionary<string, object>> DiscoverStatsAsync();

    [Get("/stats/{projectId}")]
    Task<Dictionary<string, object>> GetProjectStatisticsAsync(int projectId);
}
