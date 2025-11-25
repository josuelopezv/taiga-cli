using ModelContextProtocol.Server;
using System.ComponentModel;

namespace Taiga.Mcp.Tools;

[McpServerToolType]
public class EpicTool(IServiceProvider serviceProvider) : BaseTool(serviceProvider)
{
    [McpServerTool(Name = "ListEpics", ReadOnly = true, Destructive = false), Description("List epics (optionally filtered by project)")]
    public async Task<string> ListAsync([Description("Project ID to filter by")] int? project = null)
    {
        try
        {
            await EnsureAuthenticated();
            var epics = await Api.GetEpicsAsync(project);

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

    [McpServerTool(Name = "GetEpic", ReadOnly = true, Destructive = false), Description("Get epic by ID")]
    public async Task<string> GetAsync([Description("Epic ID")] int id, [Description("Project ID to filter by")] int project)
    {
        try
        {
            await EnsureAuthenticated();
            var epic = await Api.GetEpicAsync(id, project);
            return $"Epic Details:\n{epic}";
        }
        catch (Exception ex)
        {
            return $"Error fetching epic: {ex.Message}";
        }
    }

    [McpServerTool(Name = "CreateEpic"), Description("Create a new epic")]
    public async Task<string> CreateAsync(
    [Description("Project ID")] int project,
    [Description("Subject/title")] string subject,
    [Description("Description (support markdown)")] string? description = null,
    [Description("Status Name (e.g. \"New\", \"In Progress\")")] string? status = null,
    [Description("Assigned username")] string? assignedTo = null)
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
                data["status"] = GetStatusFromName(status, StatusType.EpicStatus, project);

            if (!string.IsNullOrWhiteSpace(assignedTo))
                data["assigned_to"] = GetUserIdFromUsername(assignedTo, project);

            var epic = await Api.CreateEpicAsync(data);
            return $"Epic created successfully:\n{epic}";
        }
        catch (Exception ex)
        {
            return $"Error creating epic: {ex.Message}";
        }
    }

    [McpServerTool(Name = "EditEpic"), Description("Edit an epic by ID")]
    public async Task<string> EditAsync(
        [Description("Epic ID")] int refid,
        [Description("Project ID")] int project,
        [Description("Subject/title")] string? subject = null,
        [Description("Description (support markdown)")] string? description = null,
        [Description("Status Name (e.g. \"New\", \"In Progress\")")] string? status = null,
        [Description("Assigned username")] string? assignedTo = null)
    {
        try
        {
            await EnsureAuthenticated();
            // Get the epic by ref to obtain its ID
            var epic = await Api.GetEpicAsync(refid, project);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (!string.IsNullOrWhiteSpace(status))
                data["status"] = GetStatusFromName(status, StatusType.EpicStatus, epic.Project);

            if (!string.IsNullOrWhiteSpace(assignedTo))
                data["assigned_to"] = GetUserIdFromUsername(assignedTo, epic.Project);

            if (data.Count == 0)
            {
                return "No fields to update. Please specify at least one field to modify.";
            }

            var updatedEpic = await Api.UpdateEpicAsync(epic.Id, data);
            return $"Epic updated successfully:\n{updatedEpic}";
        }
        catch (Exception ex)
        {
            return $"Error updating epic: {ex.Message}";
        }
    }

}
