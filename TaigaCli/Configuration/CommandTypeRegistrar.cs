using Cocona;
using Cocona.Builder;
using System.Reflection;

namespace TaigaCli.Configuration;

/// <summary>
/// Registers command types as Cocona subcommands.
/// Follows Single Responsibility Principle - only responsible for command registration.
/// </summary>
public static class CommandTypeRegistrar
{
    /// <summary>
    /// Registers all discovered command types as subcommands with Cocona.
    /// </summary>
    /// <param name="app">The Cocona commands builder.</param>
    /// <param name="serviceProvider">The service provider for dependency injection.</param>
    public static void RegisterCommandTypes(ICoconaCommandsBuilder app, IServiceProvider serviceProvider)
    {
        var commandTypes = CommandDiscovery.DiscoverCommandTypes();
        foreach (var commandType in commandTypes)
        {
            var subCommandName = CommandDiscovery.GetSubCommandName(commandType);
            RegisterCommandType(app, commandType, subCommandName, serviceProvider);
        }
    }

    /// <summary>
    /// Registers a single command type as a subcommand.
    /// </summary>
    private static void RegisterCommandType(
        ICoconaCommandsBuilder app,
        Type commandType,
        string subCommandName,
        IServiceProvider serviceProvider) =>
            app.AddSubCommand(subCommandName, subCommandBuilder =>
            {
                var commandMethods = GetCommandMethods(commandType);
                foreach (var method in commandMethods)
                {
                    var commandName = GetCommandName(method);
                    var delegateType = DelegateTypeResolver.GetDelegateType(method);
                    if (delegateType != null)
                    {
                        var factoryDelegate = ScopedDelegateFactory.CreateScopedDelegate(
                            commandType,
                            method,
                            delegateType,
                            serviceProvider);
                        if (factoryDelegate != null)
                            subCommandBuilder.AddCommand(commandName, factoryDelegate);
                    }
                }
            });

    /// <summary>
    /// Gets all methods with the [Command] attribute from a command type.
    /// </summary>
    private static IEnumerable<MethodInfo> GetCommandMethods(Type commandType) =>
        commandType
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.GetCustomAttribute<CommandAttribute>() != null);

    /// <summary>
    /// Gets the command name from a method's [Command] attribute or derives it from the method name.
    /// </summary>
    private static string GetCommandName(MethodInfo method)
    {
        var commandAttr = method.GetCustomAttribute<CommandAttribute>();
        return commandAttr?.Name ?? method.Name.ToLowerInvariant().Replace("async", "");
    }
}

