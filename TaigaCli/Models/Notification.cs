//#nullable disable
namespace TaigaCli.Models;

public class Notification
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("read")]
    public bool Read { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("data")]
    public Dictionary<string, object>? Data { get; set; }
}

