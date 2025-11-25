global using Taiga.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Taiga.Api;
using Taiga.Api.Models;

namespace Taiga.Mcp.Tools;

public abstract class BaseTool(IServiceProvider serviceProvider)
{
    protected readonly ILogger Logger = serviceProvider.GetRequiredService<ILogger<BaseTool>>();
    protected readonly IAuthService AuthService = serviceProvider.GetRequiredService<IAuthService>();
    protected readonly ITaigaApi Api = serviceProvider.GetRequiredService<ITaigaApi>();

    protected async Task EnsureAuthenticated()
    {
        Logger.LogDebug("Checking authentication status");
        var token = await AuthService.GetTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
        {
            Logger.LogWarning("Authentication check failed: no token found");
            throw new InvalidOperationException("Please run auth login first.");
        }
        Logger.LogDebug("Authentication check passed");
    }

    protected async Task<int> GetStatusFromName(string name, StatusType statusType, int projectId)
    {
        Logger.LogDebug("Looking up {StatusType} '{StatusName}' for project {ProjectId}", statusType, name, projectId);
        var statuses = await GetStatusesAsync(statusType, projectId);
        var status = statuses.FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
        if (status == null)
        {
            var availableNames = string.Join(", ", statuses.Select(s => s.Name));
            Logger.LogWarning("Status '{StatusName}' not found for type {StatusType} in project {ProjectId}. Available options: {AvailableOptions}",
                name, statusType, projectId, availableNames);
            throw new KeyNotFoundException($"Status '{name}' not found for type {statusType} in project {projectId}. Available options: {availableNames}");
        }
        Logger.LogDebug("Found {StatusType} '{StatusName}' with ID {StatusId}", statusType, name, status.Id);
        return status.Id;
    }

    private async Task<List<Status>> GetStatusesAsync(StatusType statusType, int projectId)
    {
        Logger.LogDebug("Fetching {StatusType} statuses for project {ProjectId}", statusType, projectId);
        var statuses = statusType switch
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
        Logger.LogDebug("Fetched {Count} {StatusType} statuses for project {ProjectId}", statuses.Count, statusType, projectId);
        return statuses;
    }

    protected async Task<int> GetUserIdFromUsername(string username, int projectId)
    {
        Logger.LogDebug("Looking up user '{Username}' in project {ProjectId}", username, projectId);
        var users = await Api.GetUsersAsync(projectId);
        var user = users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
        if (user == null)
        {
            var availableUsernames = string.Join(", ", users.Select(u => u.Username));
            Logger.LogWarning("User '{Username}' not found in project {ProjectId}. Available users: {AvailableUsers}",
                username, projectId, availableUsernames);
            throw new KeyNotFoundException($"User '{username}' not found in project {projectId}. Available users: {availableUsernames}");
        }
        Logger.LogDebug("Found user '{Username}' with ID {UserId}", username, user.Id);
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