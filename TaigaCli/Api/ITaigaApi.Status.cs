using Refit;
using TaigaCli.Models;

namespace TaigaCli.Api;

public partial interface ITaigaApi
{
    [Get("/severities")]
    Task<List<Severity>> GetSeveritiesAsync([Query] int? project = null);

    [Get("/priorities")]
    Task<List<Priority>> GetPrioritiesAsync([Query] int? project = null);

    [Get("/issue-statuses")]
    Task<List<Status>> GetIssueStatusesAsync([Query] int? project = null);

    [Get("/issue-types")]
    Task<List<IssueType>> GetIssueTypesAsync([Query] int? project = null);

    [Get("/task-statuses")]
    Task<List<Status>> GetTaskStatusesAsync([Query] int? project = null);

    [Get("/userstory-statuses")]
    Task<List<Status>> GetUserStoryStatusesAsync([Query] int? project = null);
}
