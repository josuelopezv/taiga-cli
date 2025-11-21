using Refit;

namespace TaigaCli.Api;

public partial interface ITaigaApi
{
    [Get("/search")]
    Task<Dictionary<string, object>> SearchProjectAsync([Query] int project,
                                                        [Query] string text);
}
