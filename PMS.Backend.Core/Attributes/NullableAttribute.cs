// -----------------------------------------------------------------------
// <copyright file="NullableAttribute.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices;

/// <summary>
/// An attribute indicating whether a property is nullable or not.
/// </summary>
/// <remarks>
/// This file is needed in this namespace because the attribute is not shipped with the compiler.
/// Some tests, however, require it, therefore, it is added here which will replace the implicit
/// compiler NullableAttribute.
/// </remarks>
[ExcludeFromCodeCoverage]
public class NullableAttribute : Attribute
{
    /// <summary>
    /// Flags indicating whether the property is nullable.
    /// </summary>
    public readonly byte[] NullableFlags;

    /// <summary>
    /// Create a new instance of the <see cref="NullableAttribute"/> class.
    /// </summary>
    /// <param name="flag">Is the associated property nullable.</param>
    public NullableAttribute(byte flag)
    {
        NullableFlags = new[] { flag };
    }

    /// <summary>
    /// Create a new instance of the <see cref="NullableAttribute"/> class.
    /// </summary>
    /// <param name="flags">Is the associated property nullable.</param>
    public NullableAttribute(byte[] flags)
    {
        NullableFlags = flags;
    }
}
