global using Taiga.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

public abstract class BaseTool(IServiceProvider serviceProvider)
{
    protected readonly IAuthService AuthService = serviceProvider.GetRequiredService<IAuthService>();
    protected readonly ITaigaApi Api = serviceProvider.GetRequiredService<ITaigaApi>();

    protected async Task EnsureAuthenticated()
    {
        var token = await AuthService.GetTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
            throw new InvalidOperationException("Please run auth login first.");
    }

    protected async Task<int> GetStatusFromName(string name, StatusType statusType, int projectId) => statusType switch
    {
        StatusType.IssueStatus => (await Api.GetIssueStatusesAsync(projectId)).FirstOrDefault(s => s.Name == name)?.Id,
        StatusType.TaskStatus => (await Api.GetTaskStatusesAsync(projectId)).FirstOrDefault(s => s.Name == name)?.Id,
        StatusType.UserStoryStatus => (await Api.GetUserStoryStatusesAsync(projectId)).FirstOrDefault(s => s.Name == name)?.Id,
        StatusType.EpicStatus => (await Api.GetEpicStatusesAsync(projectId)).FirstOrDefault(s => s.Name == name)?.Id,
        StatusType.IssueType => (await Api.GetIssueTypesAsync(projectId)).FirstOrDefault(s => s.Name == name)?.Id,
        StatusType.Severity => (await Api.GetSeveritiesAsync(projectId)).FirstOrDefault(s => s.Name == name)?.Id,
        StatusType.Priority => (await Api.GetPrioritiesAsync(projectId)).FirstOrDefault(s => s.Name == name)?.Id,
        _ => null
    } ?? throw new Exception($"Status {name} not found for type {statusType} in project {projectId}, Run GetAvailableStatus Tool to get the list of available statuses");

    protected async Task<int> GetUserIdFromUsername(string username, int projectId) =>
        (await Api.GetUsersAsync(projectId)).FirstOrDefault(u => u.Username == username)?.Id
        ?? throw new Exception($"User {username} not found in project {projectId}. Run ListUsers tool to see available users.");

}

public enum StatusType
{
    IssueStatus,
    TaskStatus,
    UserStoryStatus,
    EpicStatus,
    IssueType,
    Severity,
    Priority,
}