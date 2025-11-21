using Refit;
using TaigaCli.Models;

namespace TaigaCli.Api;

public interface ITaigaApi
{
    // Authentication
    [Post("/auth")]
    Task<AuthResponse> AuthenticateAsync([Body] AuthRequest request);

    // Users
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

    // Projects
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

    // User Stories
    [Get("/userstories")]
    Task<List<UserStory>> GetUserStoriesAsync([Query] int? project = null);

    [Get("/userstories/by_ref")]
    Task<UserStory> GetUserStoryAsync([Query] int @ref, [Query] int? project = null);

    [Post("/userstories")]
    Task<UserStory> CreateUserStoryAsync([Body] object userStoryData);

    [Patch("/userstories/{id}")]
    Task<UserStory> UpdateUserStoryAsync(int id, [Body] object userStoryData);

    [Delete("/userstories/{id}")]
    Task DeleteUserStoryAsync(int id);

    [Get("/userstories/{id}/attachments")]
    Task<List<Attachment>> GetUserStoryAttachmentsAsync(int id);

    [Post("/userstories/{id}/attachments")]
    Task<Attachment> UploadUserStoryAttachmentAsync(int id, [Body] object attachmentData);

    [Get("/userstories/{id}/attachments/{attachmentId}")]
    Task<Attachment> GetUserStoryAttachmentAsync(int id, int attachmentId);

    [Delete("/userstories/{id}/attachments/{attachmentId}")]
    Task DeleteUserStoryAttachmentAsync(int id, int attachmentId);

    [Get("/userstories/{id}/history")]
    Task<List<HistoryEntry>> GetUserStoryHistoryAsync(int id);

    [Post("/userstories/{id}/upvote")]
    Task UpvoteUserStoryAsync(int id);

    [Delete("/userstories/{id}/upvote")]
    Task RemoveUserStoryUpvoteAsync(int id);

    [Post("/userstories/{id}/downvote")]
    Task DownvoteUserStoryAsync(int id);

    [Delete("/userstories/{id}/downvote")]
    Task RemoveUserStoryDownvoteAsync(int id);

    [Get("/userstories/{id}/watchers")]
    Task<List<User>> GetUserStoryWatchersAsync(int id);

    [Post("/userstories/{id}/watchers")]
    Task AddUserStoryWatcherAsync(int id);

    [Delete("/userstories/{id}/watchers/{userId}")]
    Task RemoveUserStoryWatcherAsync(int id, int userId);

    [Post("/userstories/{id}/promote")]
    Task<Epic> PromoteUserStoryToEpicAsync(int id);

    [Post("/userstories/{id}/convert")]
    Task<Issue> ConvertUserStoryToIssueAsync(int id);

    [Get("/userstories/{id}/comments")]
    Task<List<Comment>> GetUserStoryCommentsAsync(int id);

    [Post("/userstories/{id}/comments")]
    Task<Comment> CreateUserStoryCommentAsync(int id, [Body] object commentData);

    // Tasks
    [Get("/tasks")]
    Task<List<TaigaTask>> GetTasksAsync([Query] int? project = null, [Query] int? userStory = null);

    [Get("/tasks/{id}")]
    Task<TaigaTask> GetTaskAsync(int id);

    [Post("/tasks")]
    Task<TaigaTask> CreateTaskAsync([Body] object taskData);

    [Patch("/tasks/{id}")]
    Task<TaigaTask> UpdateTaskAsync(int id, [Body] object taskData);

    [Delete("/tasks/{id}")]
    Task DeleteTaskAsync(int id);

    [Get("/tasks/{id}/attachments")]
    Task<List<Attachment>> GetTaskAttachmentsAsync(int id);

    [Post("/tasks/{id}/attachments")]
    Task<Attachment> UploadTaskAttachmentAsync(int id, [Body] object attachmentData);

    [Get("/tasks/{id}/attachments/{attachmentId}")]
    Task<Attachment> GetTaskAttachmentAsync(int id, int attachmentId);

    [Delete("/tasks/{id}/attachments/{attachmentId}")]
    Task DeleteTaskAttachmentAsync(int id, int attachmentId);

    [Get("/tasks/{id}/history")]
    Task<List<HistoryEntry>> GetTaskHistoryAsync(int id);

    [Post("/tasks/{id}/upvote")]
    Task UpvoteTaskAsync(int id);

    [Delete("/tasks/{id}/upvote")]
    Task RemoveTaskUpvoteAsync(int id);

    [Post("/tasks/{id}/downvote")]
    Task DownvoteTaskAsync(int id);

    [Delete("/tasks/{id}/downvote")]
    Task RemoveTaskDownvoteAsync(int id);

    [Get("/tasks/{id}/watchers")]
    Task<List<User>> GetTaskWatchersAsync(int id);

    [Post("/tasks/{id}/watchers")]
    Task AddTaskWatcherAsync(int id);

    [Delete("/tasks/{id}/watchers/{userId}")]
    Task RemoveTaskWatcherAsync(int id, int userId);

    [Get("/tasks/{id}/comments")]
    Task<List<Comment>> GetTaskCommentsAsync(int id);

    [Post("/tasks/{id}/comments")]
    Task<Comment> CreateTaskCommentAsync(int id, [Body] object commentData);

    // Issues
    [Get("/issues")]
    Task<List<Issue>> GetIssuesAsync([Query] int? project = null);

    [Get("/issues/{id}")]
    Task<Issue> GetIssueAsync(int id);

    [Post("/issues")]
    Task<Issue> CreateIssueAsync([Body] object issueData);

    [Patch("/issues/{id}")]
    Task<Issue> UpdateIssueAsync(int id, [Body] object issueData);

    [Delete("/issues/{id}")]
    Task DeleteIssueAsync(int id);

    [Get("/issues/{id}/attachments")]
    Task<List<Attachment>> GetIssueAttachmentsAsync(int id);

    [Post("/issues/{id}/attachments")]
    Task<Attachment> UploadIssueAttachmentAsync(int id, [Body] object attachmentData);

    [Get("/issues/{id}/attachments/{attachmentId}")]
    Task<Attachment> GetIssueAttachmentAsync(int id, int attachmentId);

    [Delete("/issues/{id}/attachments/{attachmentId}")]
    Task DeleteIssueAttachmentAsync(int id, int attachmentId);

    [Get("/issues/{id}/history")]
    Task<List<HistoryEntry>> GetIssueHistoryAsync(int id);

    [Post("/issues/{id}/upvote")]
    Task UpvoteIssueAsync(int id);

    [Delete("/issues/{id}/upvote")]
    Task RemoveIssueUpvoteAsync(int id);

    [Post("/issues/{id}/downvote")]
    Task DownvoteIssueAsync(int id);

    [Delete("/issues/{id}/downvote")]
    Task RemoveIssueDownvoteAsync(int id);

    [Get("/issues/{id}/watchers")]
    Task<List<User>> GetIssueWatchersAsync(int id);

    [Post("/issues/{id}/watchers")]
    Task AddIssueWatcherAsync(int id);

    [Delete("/issues/{id}/watchers/{userId}")]
    Task RemoveIssueWatcherAsync(int id, int userId);

    [Get("/issues/{id}/comments")]
    Task<List<Comment>> GetIssueCommentsAsync(int id);

    [Post("/issues/{id}/comments")]
    Task<Comment> CreateIssueCommentAsync(int id, [Body] object commentData);

    // Epics
    [Get("/epics")]
    Task<List<Epic>> GetEpicsAsync([Query] int? project = null);

    [Get("/epics/{id}")]
    Task<Epic> GetEpicAsync(int id);

    [Post("/epics")]
    Task<Epic> CreateEpicAsync([Body] object epicData);

    [Patch("/epics/{id}")]
    Task<Epic> UpdateEpicAsync(int id, [Body] object epicData);

    [Delete("/epics/{id}")]
    Task DeleteEpicAsync(int id);

    [Get("/epics/{id}/attachments")]
    Task<List<Attachment>> GetEpicAttachmentsAsync(int id);

    [Post("/epics/{id}/attachments")]
    Task<Attachment> UploadEpicAttachmentAsync(int id, [Body] object attachmentData);

    [Get("/epics/{id}/attachments/{attachmentId}")]
    Task<Attachment> GetEpicAttachmentAsync(int id, int attachmentId);

    [Delete("/epics/{id}/attachments/{attachmentId}")]
    Task DeleteEpicAttachmentAsync(int id, int attachmentId);

    [Get("/epics/{id}/history")]
    Task<List<HistoryEntry>> GetEpicHistoryAsync(int id);

    [Get("/epics/{id}/related-userstories")]
    Task<List<UserStory>> GetEpicRelatedUserStoriesAsync(int id);

    [Post("/epics/{id}/related-userstories")]
    Task AddEpicRelatedUserStoryAsync(int id, [Body] object userStoryData);

    [Delete("/epics/{id}/related-userstories/{userStoryId}")]
    Task RemoveEpicRelatedUserStoryAsync(int id, int userStoryId);

    [Get("/epics/{id}/comments")]
    Task<List<Comment>> GetEpicCommentsAsync(int id);

    [Post("/epics/{id}/comments")]
    Task<Comment> CreateEpicCommentAsync(int id, [Body] object commentData);

    // Milestones
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

    // Wiki
    [Get("/wiki")]
    Task<List<WikiPage>> GetWikiPagesAsync([Query] int project);

    [Get("/wiki/{id}")]
    Task<WikiPage> GetWikiPageAsync(int id);

    [Post("/wiki")]
    Task<WikiPage> CreateWikiPageAsync([Body] object wikiData);

    [Patch("/wiki/{id}")]
    Task<WikiPage> UpdateWikiPageAsync(int id, [Body] object wikiData);

    [Delete("/wiki/{id}")]
    Task DeleteWikiPageAsync(int id);

    [Get("/wiki/{id}/attachments")]
    Task<List<Attachment>> GetWikiAttachmentsAsync(int id);

    [Post("/wiki/{id}/attachments")]
    Task<Attachment> UploadWikiAttachmentAsync(int id, [Body] object attachmentData);

    [Get("/wiki/{id}/attachments/{attachmentId}")]
    Task<Attachment> GetWikiAttachmentAsync(int id, int attachmentId);

    [Delete("/wiki/{id}/attachments/{attachmentId}")]
    Task DeleteWikiAttachmentAsync(int id, int attachmentId);

    [Get("/wiki/{id}/history")]
    Task<List<HistoryEntry>> GetWikiHistoryAsync(int id);

    [Get("/wiki/{id}/comments")]
    Task<List<Comment>> GetWikiCommentsAsync(int id);

    [Post("/wiki/{id}/comments")]
    Task<Comment> CreateWikiCommentAsync(int id, [Body] object commentData);

    // Webhooks
    [Get("/webhooks")]
    Task<List<Webhook>> GetWebhooksAsync([Query] int? project = null);

    [Get("/webhooks/{id}")]
    Task<Webhook> GetWebhookAsync(int id);

    [Post("/webhooks")]
    Task<Webhook> CreateWebhookAsync([Body] object webhookData);

    [Patch("/webhooks/{id}")]
    Task<Webhook> UpdateWebhookAsync(int id, [Body] object webhookData);

    [Delete("/webhooks/{id}")]
    Task DeleteWebhookAsync(int id);

    [Post("/webhooks/{id}/test")]
    Task TestWebhookAsync(int id);

    [Get("/webhooks/{id}/logs")]
    Task<List<Dictionary<string, object>>> GetWebhookLogsAsync(int id);

    // Notifications
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

    // Search
    [Get("/search")]
    Task<Dictionary<string, object>> SearchAsync([Query] string text);

    [Get("/search/{projectId}")]
    Task<Dictionary<string, object>> SearchProjectAsync(int projectId, [Query] string text);

    // Timeline/Activity
    [Get("/timeline/{projectId}")]
    Task<List<HistoryEntry>> GetProjectTimelineAsync(int projectId);

    [Get("/timeline/{projectId}/profile")]
    Task<List<HistoryEntry>> GetProfileTimelineAsync(int projectId);

    [Get("/timeline/{projectId}/user/{userId}")]
    Task<List<HistoryEntry>> GetUserTimelineAsync(int projectId, int userId);

    // Custom Attributes
    [Get("/userstory-custom-attributes")]
    Task<List<CustomAttribute>> GetUserStoryCustomAttributesAsync([Query] int? project = null);

    [Post("/userstory-custom-attributes")]
    Task<CustomAttribute> CreateUserStoryCustomAttributeAsync([Body] object attributeData);

    [Get("/userstory-custom-attributes/{id}")]
    Task<CustomAttribute> GetUserStoryCustomAttributeAsync(int id);

    [Patch("/userstory-custom-attributes/{id}")]
    Task<CustomAttribute> UpdateUserStoryCustomAttributeAsync(int id, [Body] object attributeData);

    [Delete("/userstory-custom-attributes/{id}")]
    Task DeleteUserStoryCustomAttributeAsync(int id);

    [Get("/task-custom-attributes")]
    Task<List<CustomAttribute>> GetTaskCustomAttributesAsync([Query] int? project = null);

    [Post("/task-custom-attributes")]
    Task<CustomAttribute> CreateTaskCustomAttributeAsync([Body] object attributeData);

    [Get("/task-custom-attributes/{id}")]
    Task<CustomAttribute> GetTaskCustomAttributeAsync(int id);

    [Patch("/task-custom-attributes/{id}")]
    Task<CustomAttribute> UpdateTaskCustomAttributeAsync(int id, [Body] object attributeData);

    [Delete("/task-custom-attributes/{id}")]
    Task DeleteTaskCustomAttributeAsync(int id);

    [Get("/issue-custom-attributes")]
    Task<List<CustomAttribute>> GetIssueCustomAttributesAsync([Query] int? project = null);

    [Post("/issue-custom-attributes")]
    Task<CustomAttribute> CreateIssueCustomAttributeAsync([Body] object attributeData);

    [Get("/issue-custom-attributes/{id}")]
    Task<CustomAttribute> GetIssueCustomAttributeAsync(int id);

    [Patch("/issue-custom-attributes/{id}")]
    Task<CustomAttribute> UpdateIssueCustomAttributeAsync(int id, [Body] object attributeData);

    [Delete("/issue-custom-attributes/{id}")]
    Task DeleteIssueCustomAttributeAsync(int id);

    [Get("/epic-custom-attributes")]
    Task<List<CustomAttribute>> GetEpicCustomAttributesAsync([Query] int? project = null);

    [Post("/epic-custom-attributes")]
    Task<CustomAttribute> CreateEpicCustomAttributeAsync([Body] object attributeData);

    [Get("/epic-custom-attributes/{id}")]
    Task<CustomAttribute> GetEpicCustomAttributeAsync(int id);

    [Patch("/epic-custom-attributes/{id}")]
    Task<CustomAttribute> UpdateEpicCustomAttributeAsync(int id, [Body] object attributeData);

    [Delete("/epic-custom-attributes/{id}")]
    Task DeleteEpicCustomAttributeAsync(int id);

    // Statuses, Severities, Priorities, Types
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

    // Attachments
    [Get("/attachments/{id}")]
    Task<Attachment> GetAttachmentAsync(int id);

    [Patch("/attachments/{id}")]
    Task<Attachment> UpdateAttachmentAsync(int id, [Body] object attachmentData);

    [Delete("/attachments/{id}")]
    Task DeleteAttachmentAsync(int id);

    // History
    [Get("/history/{id}")]
    Task<HistoryEntry> GetHistoryEntryAsync(int id);

    [Get("/history/{id}/comment")]
    Task<string> GetHistoryCommentAsync(int id);

    // References
    [Get("/references/{projectId}/{ref}")]
    Task<Dictionary<string, object>> GetReferenceAsync(int projectId, string @ref);

    // Statistics
    [Get("/stats/discover")]
    Task<Dictionary<string, object>> DiscoverStatsAsync();

    [Get("/stats/{projectId}")]
    Task<Dictionary<string, object>> GetProjectStatisticsAsync(int projectId);
}
