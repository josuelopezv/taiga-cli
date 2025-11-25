using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Taiga.Mcp.Tools;

//[McpServerToolType]
public class TaskTool(IServiceProvider serviceProvider) : BaseTool(serviceProvider)
{
    [McpServerTool(Name = "ListTasks", ReadOnly = true, Destructive = false), Description("List tasks (optionally filtered by project or user story)")]
    public async Task<string> ListAsync(
        [Description("Project ID to filter by")] int? project = null,
        [Description("User Story ID to filter by")] int? userStory = null)
    {
        try
        {
            await EnsureAuthenticated();
            if (userStory.HasValue)
            {
                if (!project.HasValue)
                    return "Project ID must be specified when filtering by User Story ID.";
                userStory = (await Api.GetUserStoryAsync(userStory.Value, project)).Id; // change from refId to id
            }
            var tasks = await Api.GetTasksAsync(project, userStory);

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

    [McpServerTool(Name = "GetTask", ReadOnly = true, Destructive = false), Description("Get task by ID")]
    public async Task<string> GetAsync([Description("Task ID")] int id, [Description("Project ID to filter by")] int project)
    {
        try
        {
            await EnsureAuthenticated();
            var task = await Api.GetTaskAsync(id, project);
            return $"Task Details:\n{task}";
        }
        catch (Exception ex)
        {
            return $"Error fetching task: {ex.Message}";
        }
    }

    [McpServerTool(Name = "CreateTask"), Description("Create a new task")]
    public async Task<string> CreateAsync(
        [Description("Project ID")] int project,
        [Description("Subject/title")] string subject,
        [Description("Description")] string? description = null,
        [Description("Status Name (e.g. \"New\", \"In Progress\")")] string? status = null, // todo change to status name, use api to get id based on name
        [Description("Assigned username")] string? assignedTo = null,
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

            if (!string.IsNullOrWhiteSpace(status))
                data["status"] = GetStatusFromName(status, StatusType.TaskStatus, project);

            if (!string.IsNullOrWhiteSpace(assignedTo))
                data["assigned_to"] = GetUserIdFromUsername(assignedTo, project);

            if (userStory.HasValue)
            {
                var story = await Api.GetUserStoryAsync(userStory.Value, project);
                data["user_story"] = story.Id;
            }

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            var task = await Api.CreateTaskAsync(data);
            return $"Task created successfully:\n{task}";
        }
        catch (Exception ex)
        {
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
        [Description("Assigned username")] string? assignedTo = null,
        [Description("User Story ID")] int? userStory = null,
        [Description("Milestone ID")] int? milestone = null)
    {
        try
        {
            await EnsureAuthenticated();
            // Get the task by ref to obtain its ID
            var task = await Api.GetTaskAsync(refid, project);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (!string.IsNullOrWhiteSpace(status))
                data["status"] = GetStatusFromName(status, StatusType.TaskStatus, task.Project);

            if (!string.IsNullOrWhiteSpace(assignedTo))
                data["assigned_to"] = GetUserIdFromUsername(assignedTo, task.Project);

            if (userStory.HasValue)
                data["user_story"] = userStory.Value;

            if (milestone.HasValue)
                data["milestone"] = milestone.Value;

            if (data.Count == 0)
            {
                return "No fields to update. Please specify at least one field to modify.";
            }

            var updatedTask = await Api.UpdateTaskAsync(task.Id, data);
            return $"Task updated successfully:\n{updatedTask}";
        }
        catch (Exception ex)
        {
            return $"Error updating task: {ex.Message}";
        }
    }

}
