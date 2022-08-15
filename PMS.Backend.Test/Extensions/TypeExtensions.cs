namespace PMS.Backend.Test.Extensions;

public static class TypeExtensions
{
    public static bool IsTypeOf<T>(this Type type)
    {
        return typeof(T).IsAssignableFrom(type);
    }
}
