using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class UserStoryTool(ITaigaApi api, IAuthService authService) : BaseTool(authService)
{
    [McpServerTool, Description("List user stories (optionally filtered by project ID)")]
    public async Task<string> ListAsync([Description("Project ID to filter by")] int? project = null,
                                [Description("Epic ID to filter by")] int? epic = null)
    {
        try
        {
            await EnsureAuthenticated();
            var stories = await api.GetUserStoriesAsync(project, epic);

            if (stories.Count == 0)
            {
                var message = project.HasValue
                    ? $"No user stories found for project {project}."
                    : "No user stories found.";
                return message;
            }

            var result = $"Found {stories.Count} user story/stories:\n\n";
            foreach (var story in stories)
            {
                result += story.ToString() + "\n\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching user stories: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get user story by ID")]
    public async Task<string> GetAsync([Description("User Story ID")] int id, [Description("Project ID to filter by")] int project)
    {
        try
        {
            await EnsureAuthenticated();
            var story = await api.GetUserStoryAsync(id, project);
            return $"User Story Details:\n{story}";
        }
        catch (Exception ex)
        {
            return $"Error fetching user story: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get user story history")]
    public async Task<string> HistoryAsync([Description("User Story ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var history = await api.GetUserStoryHistoryAsync(id);
            var result = $"User Story History (ID: {id}):\n";
            foreach (var entry in history)
            {
                result += entry.ToString() + "\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching history: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get user story comments")]
    public async Task<string> CommentsAsync([Description("User Story ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var comments = await api.GetUserStoryCommentsAsync(id);
            var result = $"User Story Comments (ID: {id}):\n";
            foreach (var comment in comments)
            {
                result += comment.ToString() + "\n\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching comments: {ex.Message}";
        }
    }

    [McpServerTool, Description("Create a new user story")]
    public async Task<string> CreateAsync(
        [Description("Project ID")] int project,
        [Description("Subject/title")] string subject,
        [Description("Description (support markdown)")] string? description = null,
        [Description("Status ID")] int? status = null,
        [Description("Assigned user ID")] int? assignedTo = null,
        [Description("Milestone ID")] int? milestone = null)
    {
        try
        {
            await EnsureAuthenticated();
            var data = new Dictionary<string, object>
            {
                ["project"] = project,
                ["subject"] = subject
            };

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (status.HasValue)
                data["status"] = status.Value;

            if (assignedTo.HasValue)
                data["assigned_to"] = assignedTo.Value;

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            var story = await api.CreateUserStoryAsync(data);
            return $"User story created successfully:\n{story}";
        }
        catch (Exception ex)
        {
            return $"Error creating user story: {ex.Message}";
        }
    }

    [McpServerTool, Description("Edit a user story by ID")]
    public async Task<string> EditAsync(
        [Description("User Story ID")] int refid,
        [Description("Project ID to filter by")] int? project = null,
        [Description("Subject/title")] string? subject = null,
        [Description("Description (support markdown)")] string? description = null,
        [Description("Status ID")] int? status = null,
        [Description("Assigned user ID")] int? assignedTo = null,
        [Description("Milestone ID")] int? milestone = null)
    {
        try
        {
            await EnsureAuthenticated();
            // Get the user story by ref to obtain its ID
            var story = await api.GetUserStoryAsync(refid, project);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (status.HasValue)
                data["status"] = status.Value;

            if (assignedTo.HasValue)
                data["assigned_to"] = assignedTo.Value;

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            if (data.Count == 0)
            {
                return "No fields to update. Please specify at least one field to modify.";
            }

            var updatedStory = await api.UpdateUserStoryAsync(story.Id, data);
            return $"User story updated successfully:\n{updatedStory}";
        }
        catch (Exception ex)
        {
            return $"Error updating user story: {ex.Message}";
        }
    }

}
