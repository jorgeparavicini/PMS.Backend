using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Frontend.Agency.Controllers;
using PMS.Backend.Features.Frontend.Agency.Services;
using PMS.Backend.Features.Frontend.Agency.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Agency;

public static class Registrar
{
    public static void AddAgencyAPI(this IServiceCollection services)
    {
        // Controllers
        services.AddScoped<AgencyController>();
        
        // Services
        services.AddScoped<IAgencyService, AgencyService>();
    }
}