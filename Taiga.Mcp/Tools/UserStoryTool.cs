using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class UserStoryTool(IServiceProvider serviceProvider) : BaseTool(serviceProvider)
{
    [McpServerTool(Name = "ListUserStories", ReadOnly = true, Destructive = false), Description("List user stories (optionally filtered by project ID)")]
    public async Task<string> ListAsync([Description("Project ID to filter by")] int? project = null,
                                [Description("Epic ID to filter by")] int? epic = null)
    {
        try
        {
            Logger.LogInformation("Listing user stories for project {ProjectId}, epic {EpicId}", project, epic);
            await EnsureAuthenticated();
            if (epic.HasValue)
            {
                if (!project.HasValue)
                {
                    Logger.LogWarning("Project ID must be specified when filtering by Epic ID");
                    return "Project ID must be specified when filtering by Epic ID.";
                }
                Logger.LogDebug("Resolving epic {EpicId} to get actual ID", epic.Value);
                epic = (await Api.GetEpicAsync(epic.Value, project)).Id; // change from refId to id
            }

            var stories = await Api.GetUserStoriesAsync(project, epic);
            Logger.LogDebug("Retrieved {Count} user stories", stories.Count);

            if (stories.Count == 0)
            {
                var message = project.HasValue
                    ? $"No user stories found for project {project}."
                    : "No user stories found.";
                Logger.LogInformation("No user stories found for the specified filters");
                return message;
            }

            var result = $"Found {stories.Count} user story/stories:\n\n";
            foreach (var story in stories)
            {
                result += story.ToString() + "\n\n";
            }
            Logger.LogInformation("Successfully listed {Count} user stories", stories.Count);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching user stories for project {ProjectId}, epic {EpicId}", project, epic);
            return $"Error fetching user stories: {ex.Message}";
        }
    }

    [McpServerTool(Name = "GetUserStory", ReadOnly = true, Destructive = false), Description("Get user story by ID")]
    public async Task<string> GetAsync([Description("User Story ID")] int id, [Description("Project ID to filter by")] int project)
    {
        try
        {
            Logger.LogInformation("Getting user story {UserStoryId} from project {ProjectId}", id, project);
            await EnsureAuthenticated();
            var story = await Api.GetUserStoryAsync(id, project);
            Logger.LogInformation("Successfully retrieved user story {UserStoryId}", id);
            return $"User Story Details:\n{story}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching user story {UserStoryId} from project {ProjectId}", id, project);
            return $"Error fetching user story: {ex.Message}";
        }
    }

    [McpServerTool(Name = "CreateUserStory"), Description("Create a new user story")]
    public async Task<string> CreateAsync(
        [Description("Project ID")] int project,
        [Description("Subject/title")] string subject,
        [Description("Description (support markdown)")] string? description = null,
        [Description("Status Name (e.g. \"New\", \"In Progress\")")] string? status = null,
        [Description("Tags (comma-separated)")] string? tags = null,
        [Description("Assigned username")] string? assignedTo = null,
        [Description("Milestone ID")] int? milestone = null)
    {
        try
        {
            Logger.LogInformation("Creating new user story in project {ProjectId} with subject '{Subject}'", project, subject);
            await EnsureAuthenticated();
            var data = new Dictionary<string, object>
            {
                ["project"] = project,
                ["subject"] = subject
            };

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (!string.IsNullOrWhiteSpace(status))
            {
                Logger.LogDebug("Setting status for user story: {Status}", status);
                data["status"] = await GetStatusFromName(status, StatusType.UserStoryStatus, project);
            }

            if (!string.IsNullOrWhiteSpace(tags))
                data["tags"] = tags.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                Logger.LogDebug("Assigning user story to user: {AssignedTo}", assignedTo);
                data["assigned_to"] = await GetUserIdFromUsername(assignedTo, project);
            }

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            Logger.LogDebug("Creating user story with {FieldCount} fields", JsonSerializer.Serialize(data));
            var story = await Api.CreateUserStoryAsync(data);
            Logger.LogInformation("Successfully created user story with ID {UserStoryId}", story.Id);
            return $"User story created successfully:\n{story}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating user story in project {ProjectId}", project);
            return $"Error creating user story: {ex.Message}";
        }
    }

    [McpServerTool(Name = "EditUserStory"), Description("Edit a user story by ID")]
    public async Task<string> EditAsync(
        [Description("User Story ID")] int refid,
        [Description("Project ID")] int project,
        [Description("Subject/title")] string? subject = null,
        [Description("Description (support markdown)")] string? description = null,
        [Description("Status Name (e.g. \"New\", \"In Progress\")")] string? status = null,
        [Description("Tags (comma-separated)")] string? tags = null,
        [Description("Assigned username")] string? assignedTo = null,
        [Description("Milestone ID")] int? milestone = null)
    {
        try
        {
            Logger.LogInformation("Editing user story {UserStoryId} in project {ProjectId}", refid, project);
            await EnsureAuthenticated();
            // Get the user story by ref to obtain its ID
            var story = await Api.GetUserStoryAsync(refid, project);
            Logger.LogDebug("Retrieved user story {UserStoryId} for editing", story.Id);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (!string.IsNullOrWhiteSpace(status))
            {
                Logger.LogDebug("Updating status for user story {UserStoryId}: {Status}", story.Id, status);
                data["status"] = await GetStatusFromName(status, StatusType.UserStoryStatus, story.Project);
            }

            if (!string.IsNullOrWhiteSpace(tags))
                data["tags"] = tags.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                Logger.LogDebug("Reassigning user story {UserStoryId} to user: {AssignedTo}", story.Id, assignedTo);
                data["assigned_to"] = await GetUserIdFromUsername(assignedTo, story.Project);
            }

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            if (data.Count == 0)
            {
                Logger.LogWarning("No fields specified for updating user story {UserStoryId}", story.Id);
                return "No fields to update. Please specify at least one field to modify.";
            }

            Logger.LogDebug("Updating user story {UserStoryId} with {FieldCount} fields", story.Id, data.Count);
            var updatedStory = await Api.UpdateUserStoryAsync(story.Id, data);
            Logger.LogInformation("Successfully updated user story {UserStoryId}", story.Id);
            return $"User story updated successfully:\n{updatedStory}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error updating user story {UserStoryId} in project {ProjectId}", refid, project);
            return $"Error updating user story: {ex.Message}";
        }
    }

}
