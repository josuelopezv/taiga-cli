using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/projects")]
    Task<List<Project>> GetProjectsAsync();

    [Get("/projects/{id}")]
    Task<Project> GetProjectAsync(int id);

    [Post("/projects")]
    Task<Project> CreateProjectAsync([Body] object projectData);

    [Patch("/projects/{id}")]
    Task<Project> UpdateProjectAsync(int id, [Body] object projectData);

    [Delete("/projects/{id}")]
    Task DeleteProjectAsync(int id);

    [Get("/projects/{id}/stats")]
    Task<Dictionary<string, object>> GetProjectStatsAsync(int id);

    [Get("/projects/{id}/modules")]
    Task<Dictionary<string, bool>> GetProjectModulesAsync(int id);

    [Patch("/projects/{id}/modules")]
    Task<Dictionary<string, bool>> UpdateProjectModulesAsync(int id, [Body] Dictionary<string, bool> modules);

    [Get("/projects/{id}/memberships")]
    Task<List<ProjectMembership>> GetProjectMembershipsAsync(int id);

    [Post("/projects/{id}/memberships")]
    Task<ProjectMembership> CreateProjectMembershipAsync(int id, [Body] object membershipData);

    [Get("/projects/{id}/memberships/{membershipId}")]
    Task<ProjectMembership> GetProjectMembershipAsync(int id, int membershipId);

    [Patch("/projects/{id}/memberships/{membershipId}")]
    Task<ProjectMembership> UpdateProjectMembershipAsync(int id, int membershipId, [Body] object membershipData);

    [Delete("/projects/{id}/memberships/{membershipId}")]
    Task DeleteProjectMembershipAsync(int id, int membershipId);

    [Get("/projects/{id}/roles")]
    Task<List<ProjectRole>> GetProjectRolesAsync(int id);

    [Get("/projects/{id}/fans")]
    Task<List<User>> GetProjectFansAsync(int id);

    [Post("/projects/{id}/fans")]
    Task AddProjectFanAsync(int id);

    [Delete("/projects/{id}/fans/{userId}")]
    Task RemoveProjectFanAsync(int id, int userId);

    [Get("/projects/{id}/starred")]
    Task<bool> IsProjectStarredAsync(int id);

    [Post("/projects/{id}/starred")]
    Task StarProjectAsync(int id);

    [Delete("/projects/{id}/starred")]
    Task UnstarProjectAsync(int id);

    [Get("/projects/{id}/watchers")]
    Task<List<User>> GetProjectWatchersAsync(int id);

    [Post("/projects/{id}/watchers")]
    Task AddProjectWatcherAsync(int id);

    [Delete("/projects/{id}/watchers/{userId}")]
    Task RemoveProjectWatcherAsync(int id, int userId);
}
