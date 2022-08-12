using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PMS.Backend.Service.Filters;

/// <summary>
/// Hides properties of an entity that are marked with <see cref="IgnoreDataMemberAttribute"/>.
/// </summary>
[ExcludeFromCodeCoverage]
public class HideIgnoredDataMembersFilter : ISchemaFilter
{
    /// <inheritdoc />
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties == null)
        {
            return;
        }

        var ignoreDataMemberProperties = context.Type.GetProperties()
            .Where(x => x.GetCustomAttribute<IgnoreDataMemberAttribute>() != null);

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
