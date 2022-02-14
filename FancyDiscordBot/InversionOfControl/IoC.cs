using System.Reflection;

namespace FancyDiscordBot.InversionOfControl;

internal sealed class IoC
{
    private readonly Dictionary<Type, object> _dependencies = new();

    public void Add<T>(T instance = null) where T : class
    {
        Type type = typeof(T);

        if (instance is null)
        {
            _dependencies.Add(type, Activator.CreateInstance(type));
        }

        _dependencies.Add(type, instance);
    }

    public object Create(Type type)
    {
        ConstructorInfo ctor = type.GetConstructors().First();
        ParameterInfo[] paramsInfo = ctor.GetParameters();

        object[] parameters = new object[paramsInfo.Length];

        for (int i = 0; i < paramsInfo.Length; i++)
        {
            parameters[i] = _dependencies[paramsInfo[i].ParameterType];
        }

        return Activator.CreateInstance(type, parameters);
    }

    public T Create<T>() where T : class
    {
        Type type = typeof(T);
        return (T)Create(type);
    }
}
