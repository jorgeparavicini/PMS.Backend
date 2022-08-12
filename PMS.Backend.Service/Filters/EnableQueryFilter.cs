using Microsoft.AspNetCore.OData.Query;
using Microsoft.OpenApi.Models;
using PMS.Backend.Features.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PMS.Backend.Service.Filters;

/// <summary>
/// Adds the OData operations to the swagger endpoints.
/// </summary>
public class EnableQueryFilter : IOperationFilter
{
    private static readonly List<OpenApiParameter> Parameters =
        (new List<(string Name, string Description)>()
        {
            ("$top", "The max number of records."),
            ("$skip", "The number of records to skip."),
            ("$filter", "A function that must evaluate to true for a record to be returned."),
            ("$select", "Specifies a subset of properties to return. Use a comma separated list."),
            ("$orderby", "Determines what values are used to order a collection of records."),
            ("$expand", "Use to add related query data.")
        }).Select(pair => new OpenApiParameter()
        {
            Name = pair.Name,
            Required = false,
            Schema = new OpenApiSchema { Type = "String" },
            In = ParameterLocation.Query,
            Description = pair.Description
        })
        .ToList();

    /// <inheritdoc />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var endpointMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;
        if (endpointMetadata.Any(x => x is EnableQueryAttribute) &&
            !endpointMetadata.Any(x => x is DisableSwaggerQuery))
        {
            operation.Parameters ??= new List<OpenApiParameter>();
            foreach (var parameter in Parameters)
            {
                operation.Parameters.Add(parameter);
            }
        }
    }
}
