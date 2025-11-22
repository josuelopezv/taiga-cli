using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/history/{id}")]
    Task<HistoryEntry> GetHistoryEntryAsync(int id);

    [Get("/history/{id}/comment")]
    Task<string> GetHistoryCommentAsync(int id);
}
