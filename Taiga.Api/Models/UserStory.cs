//#nullable disable
using Taiga.Api.Extensions;

namespace Taiga.Api.Models;

public record UserStory(
        [property: JsonPropertyName("status")] int Status,
        [property: JsonPropertyName("status_extra_info")] StatusExtraInfo StatusExtraInfo,
        [property: JsonPropertyName("project")] int Project,
        [property: JsonPropertyName("project_extra_info")] ProjectExtraInfo ProjectExtraInfo,
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("ref")] int Ref,
        [property: JsonPropertyName("subject")] string Subject,
        [property: JsonPropertyName("epics")] IReadOnlyList<EpicExtraInfo>? Epics,
        [property: JsonPropertyName("description")] string? Description,
        [property: JsonPropertyName("due_date")] DateTime? DueDate,
        [property: JsonPropertyName("due_date_reason")] string? DueDateReason,
        [property: JsonPropertyName("due_date_status")] string? DueDateStatus,
        [property: JsonPropertyName("total_comments")] int TotalComments,
        [property: JsonPropertyName("tags")] IReadOnlyList<List<string>>? Tags,
        [property: JsonPropertyName("attachments")] IReadOnlyList<object> Attachments,
        [property: JsonPropertyName("assigned_to")] int? AssignedTo,
        [property: JsonPropertyName("assigned_to_extra_info")] AssignedToExtraInfo? AssignedToExtraInfo,
        [property: JsonPropertyName("owner")] int Owner,
        [property: JsonPropertyName("owner_extra_info")] OwnerExtraInfo? OwnerExtraInfo,
        [property: JsonPropertyName("is_watcher")] bool IsWatcher,
        [property: JsonPropertyName("total_watchers")] int TotalWatchers,
        [property: JsonPropertyName("is_voter")] bool IsVoter,
        [property: JsonPropertyName("total_voters")] int TotalVoters,
        [property: JsonPropertyName("milestone")] int? Milestone,
        [property: JsonPropertyName("milestone_slug")] string? MilestoneSlug,
        [property: JsonPropertyName("milestone_name")] string? MilestoneName,
        [property: JsonPropertyName("is_closed")] bool IsClosed,
        [property: JsonPropertyName("points")] Points Points,
        [property: JsonPropertyName("backlog_order")] long BacklogOrder,
        [property: JsonPropertyName("sprint_order")] long SprintOrder,
        [property: JsonPropertyName("kanban_order")] long KanbanOrder,
        [property: JsonPropertyName("created_date")] DateTime CreatedDate,
        [property: JsonPropertyName("modified_date")] DateTime ModifiedDate,
        [property: JsonPropertyName("finish_date")] DateTime? FinishDate,
        [property: JsonPropertyName("client_requirement")] bool ClientRequirement,
        [property: JsonPropertyName("team_requirement")] bool TeamRequirement,
        [property: JsonPropertyName("generated_from_issue")] int? GeneratedFromIssue,
        [property: JsonPropertyName("generated_from_task")] int? GeneratedFromTask,
        [property: JsonPropertyName("from_task_ref")] string? FromTaskRef,
        [property: JsonPropertyName("external_reference")] int? ExternalReference,
        [property: JsonPropertyName("tribe_gig")] object TribeGig, // ??
        [property: JsonPropertyName("version")] int Version,
        [property: JsonPropertyName("watchers")] IReadOnlyList<int> Watchers,
        [property: JsonPropertyName("is_blocked")] bool IsBlocked,
        [property: JsonPropertyName("blocked_note")] string BlockedNote,
        [property: JsonPropertyName("total_points")] double? TotalPoints,
        [property: JsonPropertyName("comment")] string Comment,
        [property: JsonPropertyName("origin_issue")] TaskExtraInfo? OriginIssue,
        [property: JsonPropertyName("origin_task")] TaskExtraInfo? OriginTask,
        [property: JsonPropertyName("epic_order")] int? EpicOrder,
        [property: JsonPropertyName("tasks")] IReadOnlyList<TaskExtraInfo> Tasks,
        [property: JsonPropertyName("total_attachments")] int TotalAttachments,
        [property: JsonPropertyName("swimlane")] object Swimlane, // ??
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
        sb.AppendLine($"  Owner: {OwnerExtraInfo?.FullNameDisplay}");
        if (AssignedToExtraInfo != default)
            sb.AppendLine($"  Assigned To: {AssignedToExtraInfo.FullNameDisplay}");
        sb.AppendLine($"  Subject: {Subject}");
        sb.AppendLine($"  Status: {StatusExtraInfo?.Name} Points: {Points}");
        if (DueDate != default)
            sb.AppendLine($"  Due Date: {DueDate:yyyy-MM-dd}");
        if (Tasks != null && Tasks.Count != default)
            sb.AppendLine($"  Tasks: {Tasks.Count} - {Tasks.Count} Tasks");
        if (Tags != null && Tags.Count != default)
            sb.AppendLine($"  Tags: {Tags.JoinTags(" - ")}");
        sb.AppendLine($"  Project: {Project} - {ProjectExtraInfo?.Name}");
        if (Epics != default && Epics.Count != default)
            sb.AppendLine($"  Epic(s): {Epics.Select(e => $"#{e.Ref} {e.Subject}").JoinToString(", ")}");
        if (!string.IsNullOrWhiteSpace(Description))
            sb.AppendLine($"  Description: \n{Description}");
        if (Attachments != null && Attachments.Count != default)
            sb.AppendLine($"  Attachments: {Attachments.Select(a => a.ToString()).JoinToString(" - ")}");
        return sb.ToString().TrimEnd();
    }
}
