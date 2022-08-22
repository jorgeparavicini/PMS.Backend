using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.OData;
using PMS.Backend.Features.Exceptions;
using Environment = PMS.Backend.Common.Models.Environment;

namespace PMS.Backend.Service.Extensions;

/// <summary>
/// Contains extensions for setting up Problem Details exception handling.
/// </summary>
[ExcludeFromCodeCoverage] // xUnit catches exceptions before they get converted.
public static class ProblemDetailsExtensions
{
    /// <summary>
    /// Default configuration for the <see cref="ProblemDetailsMiddleware"/>.
    /// </summary>
    /// <param name="options">The options of the problem details middleware to configure.</param>
    /// <param name="environment">The environment of the current <see cref="HttpClient"/></param>
    public static void Configure(this ProblemDetailsOptions options, string environment)
    {
        options.IncludeExceptionDetails = (_, _) =>
            environment == Environment.Development || environment == Environment.Staging;
        options.MapFluentValidationException();

        // Special Exceptions to handle
        options.MapToStatusCode<NotFoundException>(StatusCodes.Status404NotFound);
        options.MapToStatusCode<BadRequestException>(StatusCodes.Status400BadRequest);
        options.MapToStatusCode<ODataException>(StatusCodes.Status400BadRequest);

        // Finally catch all remaining exceptions
        options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
    }

    /// <summary>
    /// Custom mapping function for FluentValidation's <see cref="ValidationException"/>.
    /// </summary>
    /// <param name="options">The options of the problem details middleware to configure.</param>
    public static void MapFluentValidationException(this ProblemDetailsOptions options) =>
        options.Map<ValidationException>(
            (ctx, ex) =>
            {
                var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();

                var errors = ex.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(x => x.ErrorMessage).ToArray());

                return factory.CreateValidationProblemDetails(ctx, errors);
            });
}
