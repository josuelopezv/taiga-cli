using System.Reflection;

namespace TaigaCli.Configuration;

public static class CommandDiscovery
{
    public static IEnumerable<Type> DiscoverCommandTypes() => Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.GetCustomAttribute<SubCommandAttribute>() != null
            && !t.IsAbstract && t.IsClass);
}

