using System;

namespace PMS.Backend.Domain.ValueObjects;

public record RequiredString
{
    public string Value { get; }

    public RequiredString(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"Value cannot be null or whitespace.", paramName);
        }

        Value = value;
    }

    public static implicit operator string(RequiredString requiredString) => requiredString.Value;
}
