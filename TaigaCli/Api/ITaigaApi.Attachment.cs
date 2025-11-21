using Refit;
using TaigaCli.Models;

namespace TaigaCli.Api;

public partial interface ITaigaApi
{
    [Get("/attachments/{id}")]
    Task<Attachment> GetAttachmentAsync(int id);

    [Patch("/attachments/{id}")]
    Task<Attachment> UpdateAttachmentAsync(int id, [Body] object attachmentData);

    [Delete("/attachments/{id}")]
    Task DeleteAttachmentAsync(int id);
}
