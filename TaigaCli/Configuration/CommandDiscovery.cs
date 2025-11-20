using System.Reflection;
using TaigaCli.Commands;

namespace TaigaCli.Configuration;

public static class CommandDiscovery
{
    public static IEnumerable<Type> DiscoverCommandTypes()
    {
        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.Namespace == "TaigaCli.Commands" &&
                        t.Name.EndsWith("Commands", StringComparison.OrdinalIgnoreCase) &&
                        !t.IsAbstract &&
                        t.IsClass);
    }

    public static string GetSubCommandName(Type commandType)
    {
        return commandType.Name
            .Replace("Commands", "", StringComparison.OrdinalIgnoreCase)
            .ToLowerInvariant();
    }
}

