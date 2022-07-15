using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Frontend.Reservation.Controllers;
using PMS.Backend.Features.Frontend.Reservation.Services;
using PMS.Backend.Features.Frontend.Reservation.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Reservation;

/// <summary>
/// A registration class responsible for loading all reservation controllers and services.
/// </summary>
public static class Registrar
{
    /// <summary>
    /// Registers all required reservation services and controllers in a
    /// <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The collection which the dependencies should be added to.</param>
    public static void AddReservationAPI(this IServiceCollection services)
    {
        // Controllers
        services.AddScoped<ReservationsController>();
        
        // Services
        services.AddScoped<IReservationService, ReservationService>();
    }
}