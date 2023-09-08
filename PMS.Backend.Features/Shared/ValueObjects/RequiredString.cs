namespace PMS.Backend.Features.Shared.ValueObjects;

internal record RequiredString
{
    public string Value { get; }

    public RequiredString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"Value cannot be null or whitespace.", value);
        }

        Value = value;
    }

    public static implicit operator string(RequiredString requiredString) => requiredString.Value;
    public static implicit operator RequiredString(string value) => new(value);

    private RequiredString() : this(string.Empty)
    {
    }
}
