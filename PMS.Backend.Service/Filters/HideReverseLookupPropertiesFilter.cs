using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.OpenApi.Models;
using PMS.Backend.Core.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PMS.Backend.Service.Filters;

/// <summary>
/// Hides properties marked with <see cref="ReverseLookupAttribute"/>
/// </summary>
[ExcludeFromCodeCoverage]
public class HideReverseLookupPropertiesFilter : ISchemaFilter
{
    /// <inheritdoc />
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties == null)
        {
            return;
        }

        var reverseLookupProperties = context.Type.GetProperties()
            .Where(x => x.GetCustomAttribute<ReverseLookupAttribute>() != null);

        foreach (var reverseLookupProperty in reverseLookupProperties)
        {
            var propertytoHide = schema.Properties.Keys.SingleOrDefault(x =>
                x.ToLower() == reverseLookupProperty.Name.ToLower());

            if (propertytoHide != null)
            {
                schema.Properties.Remove(propertytoHide);
            }
        }
    }
}
