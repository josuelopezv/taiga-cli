using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/epics")]
    Task<List<Epic>> GetEpicsAsync([Query] int? project = null);

    [Get("/epics/by_ref")]
    Task<Epic> GetEpicAsync([Query] int @ref, [Query] int? project = null);

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
}
