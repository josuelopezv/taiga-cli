using Refit;
using TaigaCli.Models;

namespace TaigaCli.Api;

public partial interface ITaigaApi
{
    [Get("/search")]
    Task<SearchResult> SearchProjectAsync([Query] int project, [Query] string text);
}
