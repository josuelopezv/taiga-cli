using System.Reflection;

namespace TaigaCli.Configuration;

public static class CommandDiscovery
{
    public static IEnumerable<Type> DiscoverCommandTypes() => Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.Namespace == "TaigaCli.Commands" &&
                        t.Name.EndsWith("Commands", StringComparison.OrdinalIgnoreCase) &&
                        !t.IsAbstract &&
                        t.IsClass);

    public static string GetSubCommandName(Type commandType) => commandType.Name
            .Replace("Commands", "", StringComparison.OrdinalIgnoreCase)
            .ToLowerInvariant();
}

