using Microsoft.AspNetCore.OData.Query;

namespace PMS.Backend.Features.Attributes;

/// <summary>
/// Removes the query options from swagger of an endpoint marked with
/// <see cref="EnableQueryAttribute"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class DisableSwaggerQuery : Attribute
{
}
