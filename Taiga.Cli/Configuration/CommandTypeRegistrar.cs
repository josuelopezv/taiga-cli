using Cocona;
using Cocona.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Taiga.Cli.Configuration;

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
    public static void RegisterCommandTypes(this CoconaApp app)
    {
        var commandTypes = CommandDiscovery.DiscoverCommandTypes();
        foreach (var commandType in commandTypes)
        {
            var subCommand = commandType.GetCustomAttribute<SubCommandAttribute>()
                ?? throw new InvalidOperationException($"Command type {commandType.Name} must have SubCommandAttribute.");
            commandType.RegisterCommandType(app, subCommand.Name, app.Services, subCommand.Description ?? string.Empty);
        }
    }

    /// <summary>
    /// Registers a single command type as a subcommand.
    /// </summary>
    private static void RegisterCommandType(this Type commandType,
                                            ICoconaCommandsBuilder app,
                                            string subCommandName,
                                            IServiceProvider serviceProvider,
                                            string Description) =>
            app.AddSubCommand(subCommandName, subCommandBuilder =>
            {
                var commandMethods = commandType.GetCommandMethods();
                foreach (var method in commandMethods)
                {
                    var delegateType = method.GetDelegateType();
                    if (delegateType == null)
                        continue;
                    var factoryDelegate = commandType.CreateScopedDelegate(
                        method,
                        delegateType,
                        serviceProvider);
                    if (factoryDelegate != null)
                        subCommandBuilder.AddCommand(method.GetCommandName(), factoryDelegate);
                }
            })
            .WithDescription(Description);

    /// <summary>
    /// Gets all methods with the [Command] attribute from a command type.
    /// </summary>
    private static IEnumerable<MethodInfo> GetCommandMethods(this Type commandType) =>
        commandType
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.GetCustomAttribute<CommandAttribute>() != null);

    /// <summary>
    /// Gets the command name from a method's [Command] attribute or derives it from the method name.
    /// </summary>
    private static string GetCommandName(this MethodInfo method) =>
        method.GetCustomAttribute<CommandAttribute>()?.Name
            ?? method.Name.ToLowerInvariant().Replace("async", "");

    /// <summary>
    /// Creates a delegate for a command method using a scoped service instance.
    /// The delegate is bound to a temporary instance for reflection purposes only.
    /// The actual instance will be resolved from DI when the command is executed.
    /// </summary>
    /// <param name="commandType">The type of the command class.</param>
    /// <param name="method">The method to create a delegate for.</param>
    /// <param name="delegateType">The type of delegate to create.</param>
    /// <param name="serviceProvider">The service provider to resolve the command instance.</param>
    /// <returns>The created delegate, or null if creation fails.</returns>
    public static Delegate? CreateScopedDelegate(
        this Type commandType,
        MethodInfo method,
        Type delegateType,
        IServiceProvider serviceProvider)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            return method.CreateDelegate(delegateType, scope.ServiceProvider.GetRequiredService(commandType));
        }
        catch
        {
            return null;
        }
    }
}

