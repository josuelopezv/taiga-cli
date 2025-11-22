using Refit;
using Taiga.Api.Models;

namespace Taiga.Api;

public partial interface ITaigaApi
{
    [Get("/userstory-custom-attributes")]
    Task<List<CustomAttribute>> GetUserStoryCustomAttributesAsync([Query] int? project = null);

    [Post("/userstory-custom-attributes")]
    Task<CustomAttribute> CreateUserStoryCustomAttributeAsync([Body] object attributeData);

    [Get("/userstory-custom-attributes/{id}")]
    Task<CustomAttribute> GetUserStoryCustomAttributeAsync(int id);

    [Patch("/userstory-custom-attributes/{id}")]
    Task<CustomAttribute> UpdateUserStoryCustomAttributeAsync(int id, [Body] object attributeData);

    [Delete("/userstory-custom-attributes/{id}")]
    Task DeleteUserStoryCustomAttributeAsync(int id);

    [Get("/task-custom-attributes")]
    Task<List<CustomAttribute>> GetTaskCustomAttributesAsync([Query] int? project = null);

    [Post("/task-custom-attributes")]
    Task<CustomAttribute> CreateTaskCustomAttributeAsync([Body] object attributeData);

    [Get("/task-custom-attributes/{id}")]
    Task<CustomAttribute> GetTaskCustomAttributeAsync(int id);

    [Patch("/task-custom-attributes/{id}")]
    Task<CustomAttribute> UpdateTaskCustomAttributeAsync(int id, [Body] object attributeData);

    [Delete("/task-custom-attributes/{id}")]
    Task DeleteTaskCustomAttributeAsync(int id);

    [Get("/issue-custom-attributes")]
    Task<List<CustomAttribute>> GetIssueCustomAttributesAsync([Query] int? project = null);

    [Post("/issue-custom-attributes")]
    Task<CustomAttribute> CreateIssueCustomAttributeAsync([Body] object attributeData);

    [Get("/issue-custom-attributes/{id}")]
    Task<CustomAttribute> GetIssueCustomAttributeAsync(int id);

    [Patch("/issue-custom-attributes/{id}")]
    Task<CustomAttribute> UpdateIssueCustomAttributeAsync(int id, [Body] object attributeData);

    [Delete("/issue-custom-attributes/{id}")]
    Task DeleteIssueCustomAttributeAsync(int id);

    [Get("/epic-custom-attributes")]
    Task<List<CustomAttribute>> GetEpicCustomAttributesAsync([Query] int? project = null);

    [Post("/epic-custom-attributes")]
    Task<CustomAttribute> CreateEpicCustomAttributeAsync([Body] object attributeData);

    [Get("/epic-custom-attributes/{id}")]
    Task<CustomAttribute> GetEpicCustomAttributeAsync(int id);

    [Patch("/epic-custom-attributes/{id}")]
    Task<CustomAttribute> UpdateEpicCustomAttributeAsync(int id, [Body] object attributeData);

    [Delete("/epic-custom-attributes/{id}")]
    Task DeleteEpicCustomAttributeAsync(int id);
}
