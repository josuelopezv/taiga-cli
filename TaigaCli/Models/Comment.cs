using Newtonsoft.Json;

namespace TaigaCli.Models;

public class Comment
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("comment")]
    public string CommentText { get; set; } = string.Empty;
    
    [JsonProperty("user")]
    public int User { get; set; }
    
    [JsonProperty("created_date")]
    public DateTime CreatedDate { get; set; }
    
    [JsonProperty("modified_date")]
    public DateTime? ModifiedDate { get; set; }
}

