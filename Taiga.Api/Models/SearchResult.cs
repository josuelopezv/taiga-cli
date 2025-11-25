namespace Taiga.Api.Models;

public record SearchResult(
        [property: JsonPropertyName("epics")] IReadOnlyList<TaskExtraInfo> Epics,
        [property: JsonPropertyName("wikipages")] IReadOnlyList<TaskExtraInfo> Wikipages,
        [property: JsonPropertyName("issues")] IReadOnlyList<TaskExtraInfo> Issues,
        [property: JsonPropertyName("tasks")] IReadOnlyList<TaskExtraInfo> Tasks,
        [property: JsonPropertyName("userstories")] IReadOnlyList<TaskExtraInfo> Userstories,
        [property: JsonPropertyName("count")] int Count
    )
{
    public override string ToString()
    {
        var sb = new System.Text.StringBuilder();
        if (Epics.Count != default)
        {
            sb.AppendLine("\n  Epics:");
            foreach (var epic in Epics)
                sb.AppendLine(epic.ToString());
        }
        if (Wikipages.Count != default)
        {
            sb.AppendLine("\n  Wiki Pages:");
            foreach (var wiki in Wikipages)
                sb.AppendLine(wiki.ToString());
        }
        if (Issues.Count != default)
        {
            sb.AppendLine("\n  Issues:");
            foreach (var issue in Issues)
                sb.AppendLine(issue.ToString());
        }
        if (Tasks.Count != default)
        {
            sb.AppendLine("\n  Tasks:");
            foreach (var task in Tasks)
                sb.AppendLine(task.ToString());
        }
        if (Userstories.Count != default)
        {
            sb.AppendLine("\n  User Stories:");
            foreach (var userstory in Userstories)
                sb.AppendLine(userstory.ToString());
        }
        return sb.ToString();
    }
};
