using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Frontend.Agency.Controllers;
using PMS.Backend.Features.Frontend.Agency.Services;
using PMS.Backend.Features.Frontend.Agency.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Agency;

[ExcludeFromCodeCoverage]
public static class Registrar
{
    public static void AddAgencyAPI(this IServiceCollection services)
    {
        // Controllers
        services.AddScoped<AgenciesController>();
        
        // Services
        services.AddScoped<IAgencyService, AgencyService>();
    }
}