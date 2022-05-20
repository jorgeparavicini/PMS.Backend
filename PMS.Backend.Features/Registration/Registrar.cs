using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Registration.Controllers;

namespace PMS.Backend.Features.Registration;

public static class Registrar
{
    public static void AddRegistration(this IServiceCollection services)
    {
        // Controllers
        services.AddScoped<RegistrationController>();
    }
}