using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Registration.Controllers;
using PMS.Backend.Features.Registration.Services.Contracts;

namespace PMS.Backend.Features.Registration;

public static class Registrar
{
    public static void AddAPIFeature(this IServiceCollection services)
    {
        // Controllers
        services.AddScoped<ReservationController>();
        
        // Services
        services.AddScoped<IReservationService, IReservationService>();
    }
}