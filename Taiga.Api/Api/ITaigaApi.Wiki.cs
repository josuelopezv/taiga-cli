using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
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
}
