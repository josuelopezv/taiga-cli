using ModelContextProtocol.Server;
using System.ComponentModel;
using Taiga.Api;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class EpicTool(ITaigaApi api, IAuthService authService) : BaseTool(authService)
{
    [McpServerTool, Description("List epics (optionally filtered by project)")]
    public async Task<string> ListAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var epics = await api.GetEpicsAsync(project);

            if (epics.Count == 0)
            {
                return "No epics found.";
            }

            var result = $"Found {epics.Count} epic(s):\n\n";
            foreach (var epic in epics)
            {
                result += epic.ToString() + "\n\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching epics: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get epic by ID")]
    public async Task<string> GetAsync([Description("Epic ID")] int id, [Description("Project ID to filter by")] int project)
    {
        try
        {
            await EnsureAuthenticated();
            var epic = await api.GetEpicAsync(id, project);
            return $"Epic Details:\n{epic}";
        }
        catch (Exception ex)
        {
            return $"Error fetching epic: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get epic history")]
    public async Task<string> HistoryAsync([Description("Epic ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var history = await api.GetEpicHistoryAsync(id);
            var result = $"Epic History (ID: {id}):\n";
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

    [McpServerTool, Description("Get related user stories")]
    public async Task<string> RelatedStoriesAsync([Description("Epic ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var stories = await api.GetEpicRelatedUserStoriesAsync(id);
            var result = $"Related User Stories (Epic ID: {id}):\n";
            foreach (var story in stories)
            {
                result += story.ToString() + "\n";
            }
            return result;
        }
        catch (Exception ex)
        {
            return $"Error fetching related stories: {ex.Message}";
        }
    }

    [McpServerTool, Description("Get epic comments")]
    public async Task<string> CommentsAsync([Description("Epic ID")] int id)
    {
        try
        {
            await EnsureAuthenticated();
            var comments = await api.GetEpicCommentsAsync(id);
            var result = $"Epic Comments (ID: {id}):\n";
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

    [McpServerTool, Description("Create a new epic")]
    public async Task<string> CreateAsync(
        [Description("Project ID")] int project,
        [Description("Subject/title")] string subject,
        [Description("Description (support markdown)")] string? description = null,
        [Description("Status ID")] int? status = null,
        [Description("Assigned user ID")] int? assignedTo = null)
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

            var epic = await api.CreateEpicAsync(data);
            return $"Epic created successfully:\n{epic}";
        }
        catch (Exception ex)
        {
            return $"Error creating epic: {ex.Message}";
        }
    }

    [McpServerTool, Description("Edit an epic by ID")]
    public async Task<string> EditAsync(
        [Description("Epic ID")] int refid,
        [Description("Project ID to filter by")] int? project = null,
        [Description("Subject/title")] string? subject = null,
        [Description("Description (support markdown)")] string? description = null,
        [Description("Status ID")] int? status = null,
        [Description("Assigned user ID")] int? assignedTo = null)
    {
        try
        {
            await EnsureAuthenticated();
            // Get the epic by ref to obtain its ID
            var epic = await api.GetEpicAsync(refid, project);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (status.HasValue)
                data["status"] = status.Value;

            if (assignedTo.HasValue)
                data["assigned_to"] = assignedTo.Value;

            if (data.Count == 0)
            {
                return "No fields to update. Please specify at least one field to modify.";
            }

            var updatedEpic = await api.UpdateEpicAsync(epic.Id, data);
            return $"Epic updated successfully:\n{updatedEpic}";
        }
        catch (Exception ex)
        {
            return $"Error updating epic: {ex.Message}";
        }
    }

}
