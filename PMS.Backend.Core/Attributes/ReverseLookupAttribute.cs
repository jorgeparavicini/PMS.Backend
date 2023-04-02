using System;

namespace PMS.Backend.Core.Attributes;

/// <summary>
/// Marks that a property is a reverse lookup property for Ef Core.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ReverseLookupAttribute : Attribute
{
}
