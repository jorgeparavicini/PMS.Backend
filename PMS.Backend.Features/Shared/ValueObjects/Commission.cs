using JetBrains.Annotations;

namespace PMS.Backend.Features.Shared.ValueObjects;

internal record Commission(decimal Value)
{
    public decimal Value { get; } = Value switch
    {
        < 0 => throw new ArgumentOutOfRangeException(nameof(Value), "Commission value cannot be negative"),
        > 1 => throw new ArgumentOutOfRangeException(nameof(Value), "Commission value cannot be greater than 1"),
        _ => Value,
    };

    [UsedImplicitly]
    private Commission() : this(0)
    {
    }
}
