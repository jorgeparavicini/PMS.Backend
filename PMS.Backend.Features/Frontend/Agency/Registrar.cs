using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Frontend.Agency.Controllers;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Input.Validation;
using PMS.Backend.Features.Frontend.Agency.Services;
using PMS.Backend.Features.Frontend.Agency.Services.Contracts;

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
        services.AddScoped<IAgencyService, AgencyService>();
    }
}