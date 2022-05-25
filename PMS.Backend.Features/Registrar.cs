using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Frontend.Agency;
using PMS.Backend.Features.Frontend.Reservation;
using PMS.Backend.Features.Frontend.Reservation.Controllers;
using PMS.Backend.Features.Reservation.Services.Contracts;

namespace PMS.Backend.Features;

public static class Registrar
{
    public static void AddAPI(this IServiceCollection services)
    {
        services.AddAgencyAPI();
        services.AddReservationAPI();
    }
}