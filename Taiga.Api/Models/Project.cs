//#nullable disable
using System.Text.Json;

namespace Taiga.Api.Models;

public record Project(
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("anon_permissions")] IReadOnlyList<object> AnonPermissions,
    [property: JsonPropertyName("blocked_code")] object BlockedCode,
    [property: JsonPropertyName("created_date")] DateTime CreatedDate,
    [property: JsonPropertyName("creation_template")] int CreationTemplate,
    [property: JsonPropertyName("default_epic_status")] int DefaultEpicStatus,
    [property: JsonPropertyName("default_issue_status")] int DefaultIssueStatus,
    [property: JsonPropertyName("default_issue_type")] int DefaultIssueType,
    [property: JsonPropertyName("default_points")] int DefaultPoints,
    [property: JsonPropertyName("default_priority")] int DefaultPriority,
    [property: JsonPropertyName("default_severity")] int DefaultSeverity,
    [property: JsonPropertyName("default_task_status")] int DefaultTaskStatus,
    [property: JsonPropertyName("default_us_status")] int DefaultUsStatus,
    [property: JsonPropertyName("i_am_admin")] bool IAmAdmin,
    [property: JsonPropertyName("i_am_member")] bool IAmMember,
    [property: JsonPropertyName("i_am_owner")] bool IAmOwner,
    [property: JsonPropertyName("is_backlog_activated")] bool IsBacklogActivated,
    [property: JsonPropertyName("is_contact_activated")] bool IsContactActivated,
    [property: JsonPropertyName("is_epics_activated")] bool IsEpicsActivated,
    [property: JsonPropertyName("is_fan")] bool IsFan,
    [property: JsonPropertyName("is_featured")] bool IsFeatured,
    [property: JsonPropertyName("is_issues_activated")] bool IsIssuesActivated,
    [property: JsonPropertyName("is_kanban_activated")] bool IsKanbanActivated,
    [property: JsonPropertyName("is_looking_for_people")] bool IsLookingForPeople,
    [property: JsonPropertyName("is_private")] bool IsPrivate,
    [property: JsonPropertyName("is_watcher")] bool IsWatcher,
    [property: JsonPropertyName("is_wiki_activated")] bool IsWikiActivated,
    [property: JsonPropertyName("logo_big_url")] object LogoBigUrl,
    [property: JsonPropertyName("logo_small_url")] object LogoSmallUrl,
    [property: JsonPropertyName("looking_for_people_note")] string LookingForPeopleNote,
    [property: JsonPropertyName("members")] IReadOnlyList<object>? Members, // on list is  int on detail is User
    [property: JsonPropertyName("modified_date")] DateTime ModifiedDate,
    //[property: JsonPropertyName("my_homepage")] bool? MyHomepage,
    [property: JsonPropertyName("my_permissions")] IReadOnlyList<string> MyPermissions,
    [property: JsonPropertyName("notify_level")] int NotifyLevel,
    [property: JsonPropertyName("owner")] OwnerExtraInfo Owner,
    [property: JsonPropertyName("public_permissions")] IReadOnlyList<object> PublicPermissions,
    [property: JsonPropertyName("tags")] IReadOnlyList<object> Tags,
    //[property: JsonPropertyName("tags_colors")] TagsColors TagsColors,
    [property: JsonPropertyName("total_activity")] int TotalActivity,
    [property: JsonPropertyName("total_activity_last_month")] int TotalActivityLastMonth,
    [property: JsonPropertyName("total_activity_last_week")] int TotalActivityLastWeek,
    [property: JsonPropertyName("total_activity_last_year")] int TotalActivityLastYear,
    [property: JsonPropertyName("total_closed_milestones")] int TotalClosedMilestones,
    [property: JsonPropertyName("total_fans")] int TotalFans,
    [property: JsonPropertyName("total_fans_last_month")] int TotalFansLastMonth,
    [property: JsonPropertyName("total_fans_last_week")] int TotalFansLastWeek,
    [property: JsonPropertyName("total_fans_last_year")] int TotalFansLastYear,
    [property: JsonPropertyName("total_milestones")] object TotalMilestones,
    [property: JsonPropertyName("total_story_points")] object TotalStoryPoints,
    [property: JsonPropertyName("total_watchers")] int TotalWatchers,
    [property: JsonPropertyName("totals_updated_datetime")] DateTime TotalsUpdatedDatetime,
    [property: JsonPropertyName("videoconferences")] object Videoconferences,
    [property: JsonPropertyName("videoconferences_extra_data")] object VideoconferencesExtraData
    )
{
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"  ID: {Id}");
        sb.AppendLine($"  Name: {Name}");
        sb.AppendLine($"  Slug: {Slug}");
        if (Members != default && Members.Count != default)
            sb.AppendLine($"  Members: {GetMemberNames(Members)}");
        if (!string.IsNullOrWhiteSpace(Description))
            sb.AppendLine($"  Description: \n{Description}");
        return sb.ToString().TrimEnd();
    }

    private static string? GetMemberNames(IReadOnlyList<object>? members)
    {
        if (members == null || (members.Count == 0 && members[0].GetType() == typeof(int)))
            return null;
        var names = new List<string>();
        foreach (var member in members)
            if (member is JsonElement jsonElement)
                names.Add(jsonElement.GetProperty("username").ToString());
        return string.Join(", ", names);
    }
}
