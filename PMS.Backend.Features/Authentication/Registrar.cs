using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Authentication.Controllers;

namespace PMS.Backend.Features.Authentication
{
    public static class Registrar
    {
        public static void AddAuthenticationFeature(this IServiceCollection services)
        {
            // Controllers
            services.AddScoped<AuthenticationController>();
        }
    }
}
