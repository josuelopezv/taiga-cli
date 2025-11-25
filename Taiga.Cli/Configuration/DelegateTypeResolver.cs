using System.Reflection;

namespace Taiga.Cli.Configuration;

/// <summary>
/// Resolves delegate types from method information.
/// Follows Single Responsibility Principle - only responsible for delegate type resolution.
/// </summary>
public static class DelegateTypeResolver
{
    /// <summary>
    /// Gets the appropriate delegate type for a given method.
    /// </summary>
    /// <param name="method">The method to get the delegate type for.</param>
    /// <returns>The delegate type, or null if the method signature is not supported.</returns>
    public static Type? GetDelegateType(this MethodInfo method)
    {
        var parameters = method.GetParameters();
        var returnType = method.ReturnType;
        var paramCount = parameters.Length;
        if (returnType == typeof(Task))
        {
            // Func<T1, T2, ..., Task>
            var typeArgs = parameters.Select(p => p.ParameterType).Append(typeof(Task)).ToArray();
            var funcType = GetFuncType(paramCount + 1)
                ?? throw new DelegateTypeException("Unsupported number of parameters for Func delegate.");
            // If it's a generic type definition, make it generic; otherwise return as-is
            return funcType.IsGenericTypeDefinition ? funcType.MakeGenericType(typeArgs) : funcType;
        }
        else if (returnType == typeof(void))
        {
            // Action<T1, T2, ...>
            var typeArgs = parameters.Select(p => p.ParameterType).ToArray();
            var actionType = GetActionType(paramCount)
                ?? throw new DelegateTypeException("Unsupported number of parameters for Action delegate.");
            // If it's a generic type definition, make it generic; otherwise return as-is
            return actionType.IsGenericTypeDefinition ? actionType.MakeGenericType(typeArgs) : actionType;
        }
        throw new DelegateTypeException("Only methods returning Task or void are supported.");
    }

    /// <summary>
    /// Gets the Func type definition for the given number of generic arguments.
    /// </summary>
    private static Type? GetFuncType(int genericArgCount) =>
        // Func types: Func<TResult>, Func<T1, TResult>, Func<T1, T2, TResult>, etc.
        // genericArgCount is the total number of type parameters
        genericArgCount switch
        {
            0 => null, // Func with no args doesn't exist
            1 => typeof(Func<>),
            2 => typeof(Func<,>),
            3 => typeof(Func<,,>),
            4 => typeof(Func<,,,>),
            5 => typeof(Func<,,,,>),
            6 => typeof(Func<,,,,,>),
            7 => typeof(Func<,,,,,,>),
            8 => typeof(Func<,,,,,,,>),
            9 => typeof(Func<,,,,,,,,>),
            10 => typeof(Func<,,,,,,,,,>),
            11 => typeof(Func<,,,,,,,,,,>),
            12 => typeof(Func<,,,,,,,,,,,>),
            13 => typeof(Func<,,,,,,,,,,,,>),
            14 => typeof(Func<,,,,,,,,,,,,,>),
            15 => typeof(Func<,,,,,,,,,,,,,,>),
            16 => typeof(Func<,,,,,,,,,,,,,,,>),
            17 => typeof(Func<,,,,,,,,,,,,,,,,>),
            _ => null
        };

    /// <summary>
    /// Gets the Action type definition for the given number of generic arguments.
    /// </summary>
    private static Type? GetActionType(int genericArgCount) =>
        // Action types: Action, Action<T1>, Action<T1, T2>, etc.
        // genericArgCount is the number of type parameters
        genericArgCount switch
        {
            0 => typeof(Action),
            1 => typeof(Action<>),
            2 => typeof(Action<,>),
            3 => typeof(Action<,,>),
            4 => typeof(Action<,,,>),
            5 => typeof(Action<,,,,>),
            6 => typeof(Action<,,,,,>),
            7 => typeof(Action<,,,,,,>),
            8 => typeof(Action<,,,,,,,>),
            9 => typeof(Action<,,,,,,,,>),
            10 => typeof(Action<,,,,,,,,,>),
            11 => typeof(Action<,,,,,,,,,,>),
            12 => typeof(Action<,,,,,,,,,,,>),
            13 => typeof(Action<,,,,,,,,,,,,>),
            14 => typeof(Action<,,,,,,,,,,,,,>),
            15 => typeof(Action<,,,,,,,,,,,,,,>),
            16 => typeof(Action<,,,,,,,,,,,,,,,>),
            _ => null
        };
}

public class DelegateTypeException(string message) : Exception(message) { }

