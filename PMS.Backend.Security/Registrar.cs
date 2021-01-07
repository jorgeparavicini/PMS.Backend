using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Security.Controllers;
using PMS.Backend.Security.Database;
using PMS.Backend.Security.Entity;

namespace PMS.Backend.Security
{
    public static class Registrar
    {
        public static void AddSecurity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSecurityDb(configuration);
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SecurityDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(ConfigureIdentity);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
            services.DisableLoginRedirects();

            // Controllers
            services.AddScoped<UserController>();
        }

        private static void AddSecurityDb(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SecurityDbContext>(
                options =>
                {
                    var connectionString =
                        new SqlConnectionStringBuilder(
                            configuration.GetConnectionString("Authentication")
                        )
                        {
                            Password = configuration["Database:Authentication:Password"],
                            UserID = configuration["Database:Authentication:User"]
                        }.ConnectionString;
                    options.UseSqlServer(connectionString);
                }
            );
        }

        private static void DisableLoginRedirects(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(
                o =>
                {
                    o.Events = new CookieAuthenticationEvents()
                    {
                        OnRedirectToLogin = (ctx) =>
                        {
                            if (ctx.Response.StatusCode == 200)
                            {
                                ctx.Response.StatusCode = 401;
                            }

                            return Task.CompletedTask;
                        },
                        OnRedirectToAccessDenied = (ctx) =>
                        {
                            if (ctx.Response.StatusCode == 200)
                            {
                                ctx.Response.StatusCode = 403;
                            }

                            return Task.CompletedTask;
                        }
                    };
                }
            );
        }

        private static void ConfigureIdentity(IdentityOptions options)
        {
            // Password settings
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 1;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;

            // Lockout Settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 3;

            // User Settings
            options.User.RequireUniqueEmail = true;
        }
    }
}
