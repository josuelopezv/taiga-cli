global using Taiga.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Taiga.Api;
using Taiga.Api.Models;

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

    protected async Task<int> GetStatusFromName(string name, StatusType statusType, int projectId)
    {
        var statuses = await GetStatusesAsync(statusType, projectId);
        var status = statuses.FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
        if (status == null)
        {
            var availableNames = string.Join(", ", statuses.Select(s => s.Name));
            throw new KeyNotFoundException($"Status '{name}' not found for type {statusType} in project {projectId}. Available options: {availableNames}");
        }
        return status.Id;
    }

    private async Task<List<Status>> GetStatusesAsync(StatusType statusType, int projectId) => statusType switch
    {
        StatusType.IssueStatus => await Api.GetIssueStatusesAsync(projectId),
        StatusType.TaskStatus => await Api.GetTaskStatusesAsync(projectId),
        StatusType.UserStoryStatus => await Api.GetUserStoryStatusesAsync(projectId),
        StatusType.EpicStatus => await Api.GetEpicStatusesAsync(projectId),
        StatusType.IssueType => await Api.GetIssueTypesAsync(projectId),
        StatusType.Severity => await Api.GetSeveritiesAsync(projectId),
        StatusType.Priority => await Api.GetPrioritiesAsync(projectId),
        _ => throw new ArgumentException($"Unsupported status type: {statusType}")
    };

    protected async Task<int> GetUserIdFromUsername(string username, int projectId)
    {
        var users = await Api.GetUsersAsync(projectId);
        var user = users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
        if (user == null)
        {
            var availableUsernames = string.Join(", ", users.Select(u => u.Username));
            throw new KeyNotFoundException($"User '{username}' not found in project {projectId}. Available users: {availableUsernames}");
        }
        return user.Id;
    }

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