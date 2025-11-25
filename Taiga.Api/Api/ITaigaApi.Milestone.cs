using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/milestones")]
    Task<List<Milestone>> GetMilestonesAsync([Query] int? project = null);

    [Get("/milestones/{id}")]
    Task<Milestone> GetMilestoneAsync(int id);

    [Post("/milestones")]
    Task<Milestone> CreateMilestoneAsync([Body] object milestoneData);

    [Patch("/milestones/{id}")]
    Task<Milestone> UpdateMilestoneAsync(int id, [Body] object milestoneData);

    [Delete("/milestones/{id}")]
    Task DeleteMilestoneAsync(int id);

    [Get("/milestones/{id}/stats")]
    Task<Dictionary<string, object>> GetMilestoneStatsAsync(int id);

    [Get("/milestones/{id}/burndown")]
    Task<Dictionary<string, object>> GetMilestoneBurndownAsync(int id);

    [Get("/milestones/{id}/userstories")]
    Task<List<UserStory>> GetMilestoneUserStoriesAsync(int id);

    [Post("/milestones/{id}/userstories")]
    Task AddMilestoneUserStoryAsync(int id, [Body] object userStoryData);

    [Delete("/milestones/{id}/userstories/{userStoryId}")]
    Task RemoveMilestoneUserStoryAsync(int id, int userStoryId);
}
