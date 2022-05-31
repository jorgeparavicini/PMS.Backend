using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features;
using PMS.Backend.Features.Frontend.Agency;
using PMS.Backend.Features.Frontend.Reservation;

[assembly: ApiConventionType(typeof(Conventions))]
namespace PMS.Backend.Features;

[ExcludeFromCodeCoverage]
public static class Registrar
{
    public static void AddAPI(this IServiceCollection services)
    {
        services.AddAgencyAPI();
        services.AddReservationAPI();
    }
}