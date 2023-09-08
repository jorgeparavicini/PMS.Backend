namespace PMS.Backend.Features.Shared;

internal class Enumeration : IComparable
{
    public string Name { get; }

    public int Id { get; }

    protected Enumeration(int id, string name)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id cannot be negative");
        }

        Id = id;
        Name = name;
    }

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>()
        where T : Enumeration =>
        typeof(T).GetFields(System.Reflection.BindingFlags.Public |
                            System.Reflection.BindingFlags.Static |
                            System.Reflection.BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>();

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        bool typeMatches = GetType() == obj.GetType();
        bool valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
        int absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
        return absoluteDifference;
    }

    public static T FromValue<T>(int value)
        where T : Enumeration
    {
        T matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
        return matchingItem;
    }

    public static T FromDisplayName<T>(string displayName)
        where T : Enumeration
    {
        T matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
        return matchingItem;
    }

    private static T Parse<T, TValue>(TValue value, string description, Func<T, bool> predicate)
        where T : Enumeration
    {
        T? matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem is not null)
        {
            return matchingItem;
        }

        var message = $"'{value}' is not a valid {description} in {typeof(T)}";
        throw new InvalidOperationException(message);
    }

    public int CompareTo(object? other) => Id.CompareTo((other as Enumeration)?.Id ?? -1);
}
