// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices;

public class NullableAttribute: Attribute
{
    public readonly byte[] NullableFlags;

    public NullableAttribute(byte flag)
    {
        NullableFlags = new byte[] { flag };
    }

    public NullableAttribute(byte[] flags)
    {
        NullableFlags = flags;
    }
}
