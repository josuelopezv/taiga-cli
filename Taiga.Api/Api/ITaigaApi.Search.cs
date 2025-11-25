using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/search")]
    Task<SearchResult> SearchProjectAsync([Query] int project, [Query] string text);
}
