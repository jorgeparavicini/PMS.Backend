using JetBrains.Annotations;

namespace PMS.Backend.Features.Shared.ValueObjects;

internal record Address(string? Street, string? City, string? State, string? Country, string? ZipCode)
{
    [UsedImplicitly]
    private Address() : this(null, null, null, null, null)
    {
    }
};
