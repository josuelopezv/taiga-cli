using Microsoft.Extensions.Logging;
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
            Logger.LogInformation("Listing epics for project {ProjectId}", project);
            await EnsureAuthenticated();
            var epics = await Api.GetEpicsAsync(project);
            Logger.LogDebug("Retrieved {Count} epics", epics.Count);

            if (epics.Count == 0)
            {
                Logger.LogInformation("No epics found for project {ProjectId}", project);
                return "No epics found.";
            }

            var result = $"Found {epics.Count} epic(s):\n\n";
            foreach (var epic in epics)
            {
                result += epic.ToString() + "\n\n";
            }
            Logger.LogInformation("Successfully listed {Count} epics", epics.Count);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching epics for project {ProjectId}", project);
            return $"Error fetching epics: {ex.Message}";
        }
    }

    [McpServerTool(Name = "GetEpic", ReadOnly = true, Destructive = false), Description("Get epic by ID")]
    public async Task<string> GetAsync([Description("Epic ID")] int id, [Description("Project ID to filter by")] int project)
    {
        try
        {
            Logger.LogInformation("Getting epic {EpicId} from project {ProjectId}", id, project);
            await EnsureAuthenticated();
            var epic = await Api.GetEpicAsync(id, project);
            Logger.LogInformation("Successfully retrieved epic {EpicId}", id);
            return $"Epic Details:\n{epic}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error fetching epic {EpicId} from project {ProjectId}", id, project);
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
            Logger.LogInformation("Creating new epic in project {ProjectId} with subject '{Subject}'", project, subject);
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
                Logger.LogDebug("Setting status for epic: {Status}", status);
                data["status"] = await GetStatusFromName(status, StatusType.EpicStatus, project);
            }

            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                Logger.LogDebug("Assigning epic to user: {AssignedTo}", assignedTo);
                data["assigned_to"] = await GetUserIdFromUsername(assignedTo, project);
            }

            Logger.LogDebug("Creating epic with {FieldCount} fields", data.Count);
            var epic = await Api.CreateEpicAsync(data);
            Logger.LogInformation("Successfully created epic with ID {EpicId}", epic.Id);
            return $"Epic created successfully:\n{epic}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating epic in project {ProjectId}", project);
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
            Logger.LogInformation("Editing epic {EpicId} in project {ProjectId}", refid, project);
            await EnsureAuthenticated();
            // Get the epic by ref to obtain its ID
            var epic = await Api.GetEpicAsync(refid, project);
            Logger.LogDebug("Retrieved epic {EpicId} for editing", epic.Id);

            var data = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(subject))
                data["subject"] = subject;

            if (!string.IsNullOrWhiteSpace(description))
                data["description"] = description;

            if (!string.IsNullOrWhiteSpace(status))
            {
                Logger.LogDebug("Updating status for epic {EpicId}: {Status}", epic.Id, status);
                data["status"] = await GetStatusFromName(status, StatusType.EpicStatus, epic.Project);
            }

            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                Logger.LogDebug("Reassigning epic {EpicId} to user: {AssignedTo}", epic.Id, assignedTo);
                data["assigned_to"] = await GetUserIdFromUsername(assignedTo, epic.Project);
            }

            if (data.Count == 0)
            {
                Logger.LogWarning("No fields specified for updating epic {EpicId}", epic.Id);
                return "No fields to update. Please specify at least one field to modify.";
            }

            Logger.LogDebug("Updating epic {EpicId} with {FieldCount} fields", epic.Id, data.Count);
            var updatedEpic = await Api.UpdateEpicAsync(epic.Id, data);
            Logger.LogInformation("Successfully updated epic {EpicId}", epic.Id);
            return $"Epic updated successfully:\n{updatedEpic}";
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error updating epic {EpicId} in project {ProjectId}", refid, project);
            return $"Error updating epic: {ex.Message}";
        }
    }

}
