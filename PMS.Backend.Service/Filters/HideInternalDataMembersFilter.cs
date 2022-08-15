using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.OpenApi.Models;
using PMS.Backend.Core.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PMS.Backend.Service.Filters;

/// <summary>
/// Hides properties of an entity that are marked with <see cref="IgnoreDataMemberAttribute"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public class HideInternalDataMembersFilter : ISchemaFilter
{
    private static readonly Type[] IgnoredTypes = new Type[]
        { typeof(IgnoreDataMemberAttribute), typeof(ReverseLookupAttribute) };

    /// <inheritdoc />
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties == null)
        {
            return;
        }

        var ignoreDataMemberProperties = context.Type.GetProperties()
            .Where(x => x.GetCustomAttributes().Any(attr => IgnoredTypes.Contains(attr.GetType())));

        foreach (var ignoreDataMemberProperty in ignoreDataMemberProperties)
        {
            var propertyToHide = schema.Properties.Keys.SingleOrDefault(x =>
                x.ToLower() == ignoreDataMemberProperty.Name.ToLower());

            if (propertyToHide != null)
            {
                schema.Properties.Remove(propertyToHide);
            }
        }
    }
}
