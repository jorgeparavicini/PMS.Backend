using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Frontend.Reservation.Controllers;
using PMS.Backend.Features.Frontend.Reservation.Services;
using PMS.Backend.Features.Frontend.Reservation.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Reservation;

public static class Registrar
{
    public static void AddReservationAPI(this IServiceCollection services)
    {
        // Controllers
        services.AddScoped<ReservationsController>();
        
        // Services
        services.AddScoped<IReservationService, ReservationService>();
    }
}