using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/tasks")]
    Task<List<TaigaTask>> GetTasksAsync([Query] int? project = null, [Query] int? userStory = null);

    [Get("/tasks/by_ref")]
    Task<TaigaTask> GetTaskAsync([Query] int @ref, [Query] int? project = null);

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
}
