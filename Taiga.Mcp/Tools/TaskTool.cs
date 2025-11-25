using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class TaskTool(IServiceProvider serviceProvider) : BaseTool(serviceProvider)
{
    [McpServerTool(Name = "ListTasks", ReadOnly = true, Destructive = false), Description("List tasks (optionally filtered by project or user story)")]
    public async Task<string> ListAsync(
        [Description("Project ID to filter by")] int? project = null,
        [Description("User Story ID to filter by")] int? userStory = null)
    {
        try
        {
            Logger.LogInformation("Listing tasks for project {ProjectId}, user story {UserStoryId}", project, userStory);
            await EnsureAuthenticated();
            if (userStory.HasValue)
            {
                if (!project.HasValue)
                {
                    Logger.LogWarning("Project ID must be specified when filtering by User Story ID");
                    return "Project ID must be specified when filtering by User Story ID.";
                }
                Logger.LogDebug("Resolving user story {UserStoryId} to get actual ID", userStory.Value);
                userStory = (await Api.GetUserStoryAsync(userStory.Value, project)).Id; // change from refId to id
            }
            var tasks = await Api.GetTasksAsync(project, userStory);
            Logger.LogDebug("Retrieved {Count} tasks", tasks.Count);

            if (tasks.Count == 0)
            {
                Logger.LogInformation("No tasks found for the specified filters");
                return "No tasks found.";
            }

            var result = $"Found {tasks.Count} task(s):\n\n";
            foreach (var task in tasks)
            {
                result += task.ToString() + "\n\n";
            }
            Logger.LogInformation("Successfully listed {Count} tasks", tasks.Count);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching tasks for project {ProjectId}, user story {UserStoryId}", project, userStory);
            return $"Error fetching tasks: {ex.Message}";
        }
    }

    [McpServerTool(Name = "GetTask", ReadOnly = true, Destructive = false), Description("Get task by ID")]
    public async Task<string> GetAsync([Description("Task ID")] int id, [Description("Project ID to filter by")] int project)
    {
        try
        {
            Logger.LogInformation("Getting task {TaskId} from project {ProjectId}", id, project);
            await EnsureAuthenticated();
            var task = await Api.GetTaskAsync(id, project);
            Logger.LogInformation("Successfully retrieved task {TaskId}", id);
            return $"Task Details:\n{task}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching task {TaskId} from project {ProjectId}", id, project);
            return $"Error fetching task: {ex.Message}";
        }
    }

    [McpServerTool(Name = "CreateTask"), Description("Create a new task")]
    public async Task<string> CreateAsync(
        [Description("Project ID")] int project,
        [Description("Subject/title")] string subject,
        [Description("Description")] string? description = null,
        [Description("Status Name (e.g. \"New\", \"In Progress\")")] string? status = null,
        [Description("Tags (comma-separated)")] string? tags = null,
        [Description("Assigned username")] string? assignedTo = null,
        [Description("User Story ID")] int? userStory = null,
        [Description("Milestone ID")] int? milestone = null)
    {
        try
        {
            Logger.LogInformation("Creating new task in project {ProjectId} with subject '{Subject}'", project, subject);
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
                Logger.LogDebug("Setting status for task: {Status}", status);
                data["status"] = await GetStatusFromName(status, StatusType.TaskStatus, project);
            }

            if (!string.IsNullOrWhiteSpace(tags))
                data["tags"] = tags.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                Logger.LogDebug("Assigning task to user: {AssignedTo}", assignedTo);
                data["assigned_to"] = await GetUserIdFromUsername(assignedTo, project);
            }

            if (userStory.HasValue)
            {
                Logger.LogDebug("Associating task with user story {UserStoryId}", userStory.Value);
                var story = await Api.GetUserStoryAsync(userStory.Value, project);
                data["user_story"] = story.Id;
            }

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            Logger.LogDebug("Creating task with {FieldCount} fields", data.Count);
            var task = await Api.CreateTaskAsync(data);
            Logger.LogInformation("Successfully created task with ID {TaskId}", task.Id);
            return $"Task created successfully:\n{task}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating task in project {ProjectId}", project);
            return $"Error creating task: {ex.Message}";
        }
    }

    [McpServerTool(Name = "EditTask"), Description("Edit a task by ID")]
    public async Task<string> EditAsync(
        [Description("Task ID")] int refid,
        [Description("Project ID")] int project,
        [Description("Subject/title")] string? subject = null,
        [Description("Description")] string? description = null,
        [Description("Status Name (e.g. \"New\", \"In Progress\")")] string? status = null,
        [Description("Tags (comma-separated)")] string? tags = null,
        [Description("Assigned username")] string? assignedTo = null,
        [Description("User Story ID")] int? userStory = null,
        [Description("Milestone ID")] int? milestone = null)
    {
        try
        {
            Logger.LogInformation("Editing task {TaskId} in project {ProjectId}", refid, project);
            await EnsureAuthenticated();
            // Get the task by ref to obtain its ID
            var task = await Api.GetTaskAsync(refid, project);
            Logger.LogDebug("Retrieved task {TaskId} for editing", task.Id);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (!string.IsNullOrWhiteSpace(status))
            {
                Logger.LogDebug("Updating status for task {TaskId}: {Status}", task.Id, status);
                data["status"] = await GetStatusFromName(status, StatusType.TaskStatus, task.Project);
            }

            if (!string.IsNullOrWhiteSpace(tags))
                data["tags"] = tags.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();

            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                Logger.LogDebug("Reassigning task {TaskId} to user: {AssignedTo}", task.Id, assignedTo);
                data["assigned_to"] = await GetUserIdFromUsername(assignedTo, task.Project);
            }

            if (userStory.HasValue)
                data["user_story"] = userStory.Value;

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            if (data.Count == 0)
            {
                Logger.LogWarning("No fields specified for updating task {TaskId}", task.Id);
                return "No fields to update. Please specify at least one field to modify.";
            }

            Logger.LogDebug("Updating task {TaskId} with {FieldCount} fields", task.Id, data.Count);
            var updatedTask = await Api.UpdateTaskAsync(task.Id, data);
            Logger.LogInformation("Successfully updated task {TaskId}", task.Id);
            return $"Task updated successfully:\n{updatedTask}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error updating task {TaskId} in project {ProjectId}", refid, project);
            return $"Error updating task: {ex.Message}";
        }
    }

}
