using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features;
using PMS.Backend.Features.Frontend.Agency;
using PMS.Backend.Features.Frontend.Reservation;

[assembly: ApiConventionType(typeof(Conventions))]
namespace PMS.Backend.Features;

/// <summary>
/// A class to register all feature endpoints.
/// </summary>
[ExcludeFromCodeCoverage]
public static class Registrar
{
    /// <summary>
    /// Registers all endpoints in this feature project.
    /// </summary>
    /// <param name="services">The collection which the dependencies should be added to.</param>
    public static void AddAPI(this IServiceCollection services)
    {
        services.AddAgencyAPI();
        services.AddReservationAPI();
    }
}