using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class TaskTool(ITaigaApi api, IAuthService authService) : BaseTool(authService)
{
    [McpServerTool, Description("List tasks (optionally filtered by project or user story)")]
    public async Task<string> ListAsync(
        [Description("Project ID to filter by")] int? project = null,
        [Description("User Story ID to filter by")] int? userStory = null)
    {
        try
        {
            await EnsureAuthenticated();
            var tasks = await api.GetTasksAsync(project, userStory);

            if (tasks.Count == 0)
            {
                return "No tasks found.";
            }

            var result = $"Found {tasks.Count} task(s):\n\n";
            foreach (var task in tasks)
            {
                result += task.ToString() + "\n\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching tasks: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get task by ID")]
    public async Task<string> GetAsync([Description("Task ID")] int id, [Description("Project ID to filter by")] int project)
    {
        try
        {
            await EnsureAuthenticated();
            var task = await api.GetTaskAsync(id, project);
            return $"Task Details:\n{task}";
        }
        catch (Exception ex)
        {
            return $"Error fetching task: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get task history")]
    public async Task<string> HistoryAsync([Description("Task ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var history = await api.GetTaskHistoryAsync(id);
            var result = $"Task History (ID: {id}):\n";
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

    [McpServerTool, Description("Get task comments")]
    public async Task<string> CommentsAsync([Description("Task ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var comments = await api.GetTaskCommentsAsync(id);
            var result = $"Task Comments (ID: {id}):\n";
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

    [McpServerTool, Description("Create a new task")]
    public async Task<string> CreateAsync(
        [Description("Project ID")] int project,
        [Description("Subject/title")] string subject,
        [Description("Description")] string? description = null,
        [Description("Status ID")] int? status = null,
        [Description("Assigned user ID")] int? assignedTo = null,
        [Description("User Story ID")] int? userStory = null,
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

            if (userStory.HasValue)
            {
                var story = await api.GetUserStoryAsync(userStory.Value, project);
                data["user_story"] = story.Id;
            }

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            var task = await api.CreateTaskAsync(data);
            return $"Task created successfully:\n{task}";
        }
        catch (Exception ex)
        {
            return $"Error creating task: {ex.Message}";
        }
    }

    [McpServerTool, Description("Edit a task by ID")]
    public async Task<string> EditAsync(
        [Description("Task ID")] int refid,
        [Description("Project ID to filter by")] int? project = null,
        [Description("Subject/title")] string? subject = null,
        [Description("Description")] string? description = null,
        [Description("Status ID")] int? status = null,
        [Description("Assigned user ID")] int? assignedTo = null,
        [Description("User Story ID")] int? userStory = null,
        [Description("Milestone ID")] int? milestone = null)
    {
        try
        {
            await EnsureAuthenticated();
            // Get the task by ref to obtain its ID
            var task = await api.GetTaskAsync(refid, project);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (status.HasValue)
                data["status"] = status.Value;

            if (assignedTo.HasValue)
                data["assigned_to"] = assignedTo.Value;

            if (userStory.HasValue)
                data["user_story"] = userStory.Value;

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            if (data.Count == 0)
            {
                return "No fields to update. Please specify at least one field to modify.";
            }

            var updatedTask = await api.UpdateTaskAsync(task.Id, data);
            return $"Task updated successfully:\n{updatedTask}";
        }
        catch (Exception ex)
        {
            return $"Error updating task: {ex.Message}";
        }
    }

}
