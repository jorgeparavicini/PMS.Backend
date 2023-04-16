﻿using System;

namespace PMS.Backend.Core.Attributes;

/// <summary>
/// Marks a property as the parent of a business object.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class BusinessObjectAttribute : Attribute
{
    /// <summary>
    /// Get the name of the endpoint for this business object.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="BusinessObjectAttribute"/>.
    /// </summary>
    /// <param name="name">The name of the endpoint for this business object.</param>
    public BusinessObjectAttribute(string name)
    {
        Name = name;
    }
}
