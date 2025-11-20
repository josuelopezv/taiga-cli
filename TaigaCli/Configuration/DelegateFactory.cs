using System.Reflection;

namespace TaigaCli.Configuration;

public static class DelegateFactory
{
    public static Delegate? CreateDelegate(MethodInfo method, object instance)
    {
        var parameters = method.GetParameters();
        var returnType = method.ReturnType;

        return returnType switch
        {
            _ when returnType == typeof(Task) => CreateTaskDelegate(method, instance, parameters),
            _ when returnType == typeof(void) => CreateActionDelegate(method, instance, parameters),
            _ => null
        };
    }

    private static Delegate? CreateTaskDelegate(MethodInfo method, object instance, ParameterInfo[] parameters) => parameters.Length switch
    {
        0 => method.CreateDelegate<Func<Task>>(instance),
        1 => method.CreateDelegate(
            typeof(Func<,>).MakeGenericType(parameters[0].ParameterType, typeof(Task)),
            instance),
        2 => method.CreateDelegate(
            typeof(Func<,,>).MakeGenericType(
                parameters[0].ParameterType,
                parameters[1].ParameterType,
                typeof(Task)),
            instance),
        3 => method.CreateDelegate(
            typeof(Func<,,,>).MakeGenericType(
                parameters[0].ParameterType,
                parameters[1].ParameterType,
                parameters[2].ParameterType,
                typeof(Task)),
            instance),
        _ => null
    };

    private static Delegate? CreateActionDelegate(MethodInfo method, object instance, ParameterInfo[] parameters) => parameters.Length switch
    {
        0 => method.CreateDelegate<Action>(instance),
        1 => method.CreateDelegate(
            typeof(Action<>).MakeGenericType(parameters[0].ParameterType),
            instance),
        2 => method.CreateDelegate(
            typeof(Action<,>).MakeGenericType(
                parameters[0].ParameterType,
                parameters[1].ParameterType),
            instance),
        3 => method.CreateDelegate(
            typeof(Action<,,>).MakeGenericType(
                parameters[0].ParameterType,
                parameters[1].ParameterType,
                parameters[2].ParameterType),
            instance),
        _ => null
    };
}

