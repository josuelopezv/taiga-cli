using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/userstories")]
    Task<List<UserStory>> GetUserStoriesAsync([Query] int? project = null, [Query] int? epic = null);

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
}
