using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PMS.Backend.Service.Filters;

/// <summary>
/// A swagger filter adding 401 Unauthorized and 403 Forbidden errors to endpoints.
/// </summary>
/// <remarks>
/// The responses will be added to all endpoints unless they have an 'AllowAnonymous' attribute.
/// </remarks>
public class SecurityRequirementsOperationFilter : IOperationFilter
{
    /// <inheritdoc />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Check if there is a method-level 'AllowAnonymous', as this overrides any
        // controller-level 'Authorize'
        var anonControllerScope = context.MethodInfo.DeclaringType!.GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>();

        var anonMethodScope = context.MethodInfo.GetCustomAttributes(true)
            .OfType<AllowAnonymousAttribute>();

        if (!anonMethodScope.Any() && !anonControllerScope.Any())
        {
            if (!operation.Responses.ContainsKey("401"))
            {
                operation.Responses.Add("401",
                    new OpenApiResponse
                    {
                        Description =
                            "If Authorization header not present, has no value or no valid jwt bearer token"
                    });
            }

            if (!operation.Responses.ContainsKey("403"))
            {
                operation.Responses.Add("403",
                    new OpenApiResponse
                        { Description = "If user not authorized to perform requested action" });
            }

            var jwtAuthScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                    { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            };

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    [jwtAuthScheme] = new List<string>()
                }
            };
        }
    }
}
