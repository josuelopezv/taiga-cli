//#nullable disable
using Taiga.Api.Extensions;

namespace Taiga.Api.Models;

public record TaigaTask(
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("project")] int Project,
    [property: JsonPropertyName("project_extra_info")] ProjectExtraInfo ProjectExtraInfo,
    [property: JsonPropertyName("ref")] int Ref,
    [property: JsonPropertyName("status")] int Status,
    [property: JsonPropertyName("status_extra_info")] StatusExtraInfo StatusExtraInfo,
    [property: JsonPropertyName("subject")] string Subject,
    [property: JsonPropertyName("user_story_extra_info")] UserStoryExtraInfo? UserStoryExtraInfo,
    [property: JsonPropertyName("assigned_to")] int? AssignedTo,
    [property: JsonPropertyName("assigned_to_extra_info")] AssignedToExtraInfo? AssignedToExtraInfo,
    [property: JsonPropertyName("attachments")] IReadOnlyList<object> Attachments,
    [property: JsonPropertyName("blocked_note")] string BlockedNote,
    [property: JsonPropertyName("blocked_note_html")] string BlockedNoteHtml,
    [property: JsonPropertyName("comment")] string Comment,
    [property: JsonPropertyName("created_date")] DateTime CreatedDate,
    [property: JsonPropertyName("description_html")] string DescriptionHtml,
    [property: JsonPropertyName("due_date")] DateTime? DueDate,
    [property: JsonPropertyName("due_date_reason")] string DueDateReason,
    [property: JsonPropertyName("due_date_status")] string DueDateStatus,
    [property: JsonPropertyName("external_reference")] int? ExternalReference,
    [property: JsonPropertyName("finished_date")] DateTime? FinishedDate,
    [property: JsonPropertyName("generated_user_stories")] object GeneratedUserStories,
    [property: JsonPropertyName("is_blocked")] bool IsBlocked,
    [property: JsonPropertyName("is_closed")] bool IsClosed,
    [property: JsonPropertyName("is_iocaine")] bool IsIocaine,
    [property: JsonPropertyName("is_voter")] bool IsVoter,
    [property: JsonPropertyName("is_watcher")] bool IsWatcher,
    [property: JsonPropertyName("milestone")] int? Milestone,
    [property: JsonPropertyName("milestone_slug")] string? MilestoneSlug,
    [property: JsonPropertyName("modified_date")] DateTime ModifiedDate,
    [property: JsonPropertyName("neighbors")] Neighbors? Neighbors,
    [property: JsonPropertyName("owner")] int Owner,
    [property: JsonPropertyName("owner_extra_info")] OwnerExtraInfo? OwnerExtraInfo,
    [property: JsonPropertyName("tags")] IReadOnlyList<List<string>>? Tags,
    [property: JsonPropertyName("taskboard_order")] long TaskboardOrder,
    [property: JsonPropertyName("total_comments")] int TotalComments,
    [property: JsonPropertyName("total_voters")] int TotalVoters,
    [property: JsonPropertyName("total_watchers")] int TotalWatchers,
    [property: JsonPropertyName("us_order")] long UsOrder,
    [property: JsonPropertyName("user_story")] int? UserStory,
    [property: JsonPropertyName("version")] int Version,
    [property: JsonPropertyName("watchers")] IReadOnlyList<int> Watchers
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
        sb.AppendLine($"  Status: {Status} - {StatusExtraInfo?.Name}");
        if (DueDate != default)
            sb.AppendLine($"  Due Date: {DueDate:yyyy-MM-dd}");
        if (Tags != null && Tags.Count != default)
            sb.AppendLine($"  Tags: {Tags.JoinTags(" - ")}");
        sb.AppendLine($"  Project: {Project} - {ProjectExtraInfo?.Name}");
        if (UserStoryExtraInfo != default)
            sb.AppendLine($"  User Story: #{UserStoryExtraInfo.Ref} {UserStoryExtraInfo.Subject}");
        if (!string.IsNullOrWhiteSpace(Description))
            sb.AppendLine($"  Description: \n{Description}");
        if (Attachments != null && Attachments.Count != default)
            sb.AppendLine($"  Attachments: {Attachments.Select(a => a.ToString()).JoinToString(" - ")}");
        return sb.ToString().TrimEnd();
    }
}
