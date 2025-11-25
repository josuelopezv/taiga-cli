//using ModelContextProtocol.Server;
//using Taiga.Api;

//namespace Taiga.Mcp.Resources;

//[McpServerResourceType]
//public class ResourceTypes(ITaigaApi api)
//{
//    [McpServerResource(UriTemplate = "taigamcp://resourcetypes", Name = "Resource Types")]
//    public static async Task<string> ListResourceTypes()
//    {
//        var resourceTypes = new List<string>
//        {
//            "Project",
//            "Issue",
//            "User",
//            "Comment",
//            "Attachment"
//        };
//        return string.Join("\n", resourceTypes);
//    }
//}
