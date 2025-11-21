//#nullable disable
using TaigaCli.Extensions;

namespace TaigaCli.Models;

public record UserStory(
        [property: JsonPropertyName("status")] int Status,
        [property: JsonPropertyName("status_extra_info")] StatusExtraInfo StatusExtraInfo,
        [property: JsonPropertyName("project")] int Project,
        [property: JsonPropertyName("project_extra_info")] ProjectExtraInfo ProjectExtraInfo,
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("ref")] int Ref,
        [property: JsonPropertyName("subject")] string Subject,
        [property: JsonPropertyName("epics")] IReadOnlyList<EpicExtraInfo> Epics,
        [property: JsonPropertyName("description")] string? Description,
        [property: JsonPropertyName("due_date")] object DueDate,
        [property: JsonPropertyName("due_date_reason")] string DueDateReason,
        [property: JsonPropertyName("due_date_status")] string DueDateStatus,
        [property: JsonPropertyName("total_comments")] int TotalComments,
        [property: JsonPropertyName("tags")] IReadOnlyList<List<string>> Tags,
        [property: JsonPropertyName("attachments")] IReadOnlyList<object> Attachments,
        [property: JsonPropertyName("assigned_to")] int? AssignedTo,
        [property: JsonPropertyName("assigned_to_extra_info")] AssignedToExtraInfo? AssignedToExtraInfo,
        [property: JsonPropertyName("owner")] int Owner,
        [property: JsonPropertyName("owner_extra_info")] OwnerExtraInfo OwnerExtraInfo,
        [property: JsonPropertyName("is_watcher")] bool IsWatcher,
        [property: JsonPropertyName("total_watchers")] int TotalWatchers,
        [property: JsonPropertyName("is_voter")] bool IsVoter,
        [property: JsonPropertyName("total_voters")] int TotalVoters,
        [property: JsonPropertyName("milestone")] object Milestone,
        [property: JsonPropertyName("milestone_slug")] object MilestoneSlug,
        [property: JsonPropertyName("milestone_name")] object MilestoneName,
        [property: JsonPropertyName("is_closed")] bool IsClosed,
        [property: JsonPropertyName("points")] Points Points,
        [property: JsonPropertyName("backlog_order")] long BacklogOrder,
        [property: JsonPropertyName("sprint_order")] long SprintOrder,
        [property: JsonPropertyName("kanban_order")] long KanbanOrder,
        [property: JsonPropertyName("created_date")] DateTime CreatedDate,
        [property: JsonPropertyName("modified_date")] DateTime ModifiedDate,
        [property: JsonPropertyName("finish_date")] object FinishDate,
        [property: JsonPropertyName("client_requirement")] bool ClientRequirement,
        [property: JsonPropertyName("team_requirement")] bool TeamRequirement,
        [property: JsonPropertyName("generated_from_issue")] object GeneratedFromIssue,
        [property: JsonPropertyName("generated_from_task")] object GeneratedFromTask,
        [property: JsonPropertyName("from_task_ref")] object FromTaskRef,
        [property: JsonPropertyName("external_reference")] object ExternalReference,
        [property: JsonPropertyName("tribe_gig")] object TribeGig,
        [property: JsonPropertyName("version")] int Version,
        [property: JsonPropertyName("watchers")] IReadOnlyList<int> Watchers,
        [property: JsonPropertyName("is_blocked")] bool IsBlocked,
        [property: JsonPropertyName("blocked_note")] string BlockedNote,
        [property: JsonPropertyName("total_points")] object TotalPoints,
        [property: JsonPropertyName("comment")] string Comment,
        [property: JsonPropertyName("origin_issue")] object OriginIssue,
        [property: JsonPropertyName("origin_task")] object OriginTask,
        [property: JsonPropertyName("epic_order")] object EpicOrder,
        [property: JsonPropertyName("tasks")] IReadOnlyList<object> Tasks,
        [property: JsonPropertyName("total_attachments")] int TotalAttachments,
        [property: JsonPropertyName("swimlane")] object Swimlane,
        [property: JsonPropertyName("assigned_users")] IReadOnlyList<int> AssignedUsers,
        [property: JsonPropertyName("blocked_note_html")] string? BlockedNoteHtml,
        [property: JsonPropertyName("description_html")] string? DescriptionHtml,
        [property: JsonPropertyName("neighbors")] Neighbors Neighbors
    )
{
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"  ID: #{Ref}");
        sb.AppendLine($"  Subject: {Subject}");
        sb.AppendLine($"  Status: {Status} - {StatusExtraInfo?.Name}");
        if (Tasks.Count != default)
            sb.AppendLine($"  Tasks: {Tasks.Count} - {Tasks.Count} Tasks");
        if (Tags.Count != default)
            sb.AppendLine($"  Tags: {Tags.JoinTags(" - ")}");
        if (!string.IsNullOrWhiteSpace(Description))
        {
            sb.AppendLine("  Description:");
            sb.AppendLine(Description);
        }
        return sb.ToString().TrimEnd();
    }
}
