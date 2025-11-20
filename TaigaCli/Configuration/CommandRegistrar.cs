using Cocona;
using Cocona.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaigaCli.Configuration;

namespace TaigaCli.Configuration;

public static class CommandRegistrar
{
    public static void RegisterCommands(ICoconaCommandsBuilder app, IServiceProvider serviceProvider)
    {
        var commandTypes = CommandDiscovery.DiscoverCommandTypes();

        foreach (var commandType in commandTypes)
        {
            var subCommandName = CommandDiscovery.GetSubCommandName(commandType);
            var commandInstance = serviceProvider.GetRequiredService(commandType);
            var commandMethods = GetCommandMethods(commandType);

            if (commandMethods.Count == 0)
                continue;

            app.AddSubCommand(subCommandName, subCommandBuilder =>
            {
                foreach (var method in commandMethods)
                {
                    var commandName = GetCommandName(method);
                    var methodDelegate = DelegateFactory.CreateDelegate(method, commandInstance);

                    if (methodDelegate != null)
                    {
                        subCommandBuilder.AddCommand(commandName, methodDelegate);
                    }
                }
            });
        }
    }

    private static List<MethodInfo> GetCommandMethods(Type commandType)
    {
        return commandType
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.GetCustomAttribute<CommandAttribute>() != null)
            .ToList();
    }

    private static string GetCommandName(MethodInfo method)
    {
        var commandAttr = method.GetCustomAttribute<CommandAttribute>();
        return commandAttr?.Name ?? method.Name.ToLowerInvariant().Replace("async", "");
    }
}

