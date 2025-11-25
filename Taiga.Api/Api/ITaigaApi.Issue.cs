using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/issues")]
    Task<List<Issue>> GetIssuesAsync([Query] int? project = null);

    [Get("/issues/by_ref")]
    Task<Issue> GetIssueAsync([Query] int @ref, [Query] int? project = null);

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
}
