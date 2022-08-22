using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Common;
using PMS.Backend.Features.Frontend.Agency.Controllers;
using PMS.Backend.Features.Frontend.Agency.Services;

namespace PMS.Backend.Features.Frontend.Agency;

/// <summary>
/// A registration class responsible for loading all agency controllers and services.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Registrar
{
    /// <summary>
    /// Registers all required agency services and controllers in a
    /// <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The collection which the dependencies should be added to.</param>
    public static void AddAgencyAPI(this IServiceCollection services)
    {
        // Controllers
        services.AddScoped<AgenciesController>();

        // Services
        services.AddScoped<Service<Core.Entities.Agency.Agency>, AgencyService>();
    }
}
