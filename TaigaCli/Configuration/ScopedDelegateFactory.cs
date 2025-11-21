using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TaigaCli.Configuration;

/// <summary>
/// Creates scoped delegates for command methods.
/// Follows Single Responsibility Principle - only responsible for delegate creation with scoped DI.
/// </summary>
public static class ScopedDelegateFactory
{
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
        Type commandType,
        MethodInfo method,
        Type delegateType,
        IServiceProvider serviceProvider)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var instance = scope.ServiceProvider.GetRequiredService(commandType);
            return method.CreateDelegate(delegateType, instance);
        }
        catch
        {
            return null;
        }
    }
}

