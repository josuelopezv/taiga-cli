using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api.Models;

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
            await EnsureAuthenticated();
            if (epic.HasValue)
            {
                if (!project.HasValue)
                    return "Project ID must be specified when filtering by Epic ID.";
                epic = (await Api.GetEpicAsync(epic.Value, project)).Id; // change from refId to id
            }

            var stories = await Api.GetUserStoriesAsync(project, epic);

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

    [McpServerTool(Name = "GetUserStory", ReadOnly = true, Destructive = false), Description("Get user story by ID")]
    public async Task<string> GetAsync([Description("User Story ID")] int id, [Description("Project ID to filter by")] int project)
    {
        try
        {
            await EnsureAuthenticated();
            var story = await Api.GetUserStoryAsync(id, project);
            return $"User Story Details:\n{story}";
        }
        catch (Exception ex)
        {
            return $"Error fetching user story: {ex.Message}";
        }
    }

    [McpServerTool(Name = "CreateUserStory"), Description("Create a new user story")]
    public async Task<string> CreateAsync(
        [Description("Project ID")] int project,
        [Description("Subject/title")] string subject,
        [Description("Description (support markdown)")] string? description = null,
        [Description("Status Name (e.g. \"New\", \"In Progress\")")] string? status = null,
        [Description("Points for Ux, Design, Front-end, Back-end")] Points? points = null,
        [Description("Assigned username")] string? assignedTo = null,
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

            if (!string.IsNullOrWhiteSpace(status))
                data["status"] = GetStatusFromName(status, StatusType.UserStoryStatus, project);

            if (points != null)
                data["points"] = points;

            if (!string.IsNullOrWhiteSpace(assignedTo))
                data["assigned_to"] = GetUserIdFromUsername(assignedTo, project);

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            var story = await Api.CreateUserStoryAsync(data);
            return $"User story created successfully:\n{story}";
        }
        catch (Exception ex)
        {
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
        [Description("Points for Ux, Design, Front-end, Back-end")] Points? points = null,
        [Description("Assigned username")] string? assignedTo = null,
        [Description("Milestone ID")] int? milestone = null)
    {
        try
        {
            await EnsureAuthenticated();
            // Get the user story by ref to obtain its ID
            var story = await Api.GetUserStoryAsync(refid, project);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (!string.IsNullOrWhiteSpace(status))
                data["status"] = GetStatusFromName(status, StatusType.UserStoryStatus, story.Project);

            if (points != null)
                data["points"] = points;

            if (!string.IsNullOrWhiteSpace(assignedTo))
                data["assigned_to"] = GetUserIdFromUsername(assignedTo, story.Project);

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            if (data.Count == 0)
            {
                return "No fields to update. Please specify at least one field to modify.";
            }

            var updatedStory = await Api.UpdateUserStoryAsync(story.Id, data);
            return $"User story updated successfully:\n{updatedStory}";
        }
        catch (Exception ex)
        {
            return $"Error updating user story: {ex.Message}";
        }
    }

}
