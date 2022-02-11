namespace SkillMatrixAPITest.DI;

public static class SimpleDI //static for simplicity
{
    private static readonly Dictionary<Type, Type>   implementations = new();
    private static readonly Dictionary<Type, object> instances       = new();

    public static void RegisterSingleton<T, U>() where T : class where U : T, new()
    {
        implementations.TryAdd(typeof(T), typeof(U));
    }

    public static T GetSingleton<T>()
    {
        var interfaceType = typeof(T);
        if (!implementations.TryGetValue(interfaceType, out var implType))
        {
            if (instances.ContainsKey(interfaceType))
            {
                implType = interfaceType;
            }
        }

        if (implType is null)
        {
            throw new ArgumentException(
                    $"Type is neither an interface nor an implementation. Please Register an implementation or interface for {implType}");
        }

        if (!instances.TryGetValue(implType, out var instance))
        {
            instance = Activator.CreateInstance(implType)!; //! bc new() constraint
            instances.Add(implType, instance);
        }

        return (T)instance;
    }
}