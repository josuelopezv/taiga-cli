//#nullable disable
namespace TaigaCli.Models;

public record Epic(
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("project")] int Project,
    [property: JsonPropertyName("project_extra_info")] ProjectExtraInfo ProjectExtraInfo,
    [property: JsonPropertyName("ref")] int Ref,
    [property: JsonPropertyName("status")] int Status,
    [property: JsonPropertyName("status_extra_info")] StatusExtraInfo StatusExtraInfo,
    [property: JsonPropertyName("subject")] string Subject
//[property: JsonPropertyName("assigned_to")] int AssignedTo,
//[property: JsonPropertyName("assigned_to_extra_info")] AssignedToExtraInfo AssignedToExtraInfo,
//[property: JsonPropertyName("attachments")] IReadOnlyList<object> Attachments,
//[property: JsonPropertyName("blocked_note")] string BlockedNote,
//[property: JsonPropertyName("blocked_note_html")] string BlockedNoteHtml,
//[property: JsonPropertyName("client_requirement")] bool ClientRequirement,
//[property: JsonPropertyName("color")] string Color,
//[property: JsonPropertyName("comment")] string Comment,
//[property: JsonPropertyName("created_date")] DateTime CreatedDate,
//[property: JsonPropertyName("description_html")] string DescriptionHtml,
//[property: JsonPropertyName("epics_order")] long EpicsOrder,
//[property: JsonPropertyName("is_blocked")] bool IsBlocked,
//[property: JsonPropertyName("is_closed")] bool IsClosed,
//[property: JsonPropertyName("is_voter")] bool IsVoter,
//[property: JsonPropertyName("is_watcher")] bool IsWatcher,
//[property: JsonPropertyName("modified_date")] DateTime ModifiedDate,
//[property: JsonPropertyName("neighbors")] Neighbors Neighbors,
//[property: JsonPropertyName("owner")] int Owner,
//[property: JsonPropertyName("owner_extra_info")] OwnerExtraInfo OwnerExtraInfo,
//[property: JsonPropertyName("tags")] IReadOnlyList<List<string>> Tags,
//[property: JsonPropertyName("team_requirement")] bool TeamRequirement,
//[property: JsonPropertyName("total_voters")] int TotalVoters,
//[property: JsonPropertyName("total_watchers")] int TotalWatchers,
//[property: JsonPropertyName("user_stories_counts")] UserStoriesCounts UserStoriesCounts,
//[property: JsonPropertyName("version")] int Version,
//[property: JsonPropertyName("watchers")] IReadOnlyList<int> Watchers
)
{
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"  ID: #{Ref}");
        sb.AppendLine($"  Subject: {Subject}");
        sb.AppendLine($"  Project: {Project} - {ProjectExtraInfo?.Name}");
        sb.AppendLine($"  Status: {Status} - {StatusExtraInfo?.Name}");
        if (!string.IsNullOrWhiteSpace(Description))
        {
            sb.AppendLine("  Description:");
            sb.AppendLine(Description);
        }
        return sb.ToString().TrimEnd();
    }
}
