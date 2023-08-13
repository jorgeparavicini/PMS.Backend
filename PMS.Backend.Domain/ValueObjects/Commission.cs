using System;

namespace PMS.Backend.Domain.ValueObjects;

public record Commission(decimal Value)
{
    public decimal Value { get; } = Value switch
    {
        < 0 => throw new ArgumentOutOfRangeException(nameof(Value), "Commission value cannot be negative"),
        > 1 => throw new ArgumentOutOfRangeException(nameof(Value), "Commission value cannot be greater than 100"),
        _ => Value,
    };
}
