using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Security.Claims;
using Detached.Mappers.EntityFramework;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PMS.Backend.Core.Database;
using PMS.Backend.Features;
using PMS.Backend.Service.Extensions;
using PMS.Backend.Service.Security;
using DbContextExtensions = PMS.Backend.Service.Extensions.DbContextExtensions;

namespace PMS.Backend.Service;

/// <summary>
/// The parent class for the entry point of the service.
/// </summary>
public static class Program
{
    /// <summary>
    /// The entry point to start the web service
    /// </summary>
    /// <param name="args">Optional command line arguments</param>
    [ExcludeFromCodeCoverage]
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add exception handling middleware
        builder.Services.AddProblemDetails(options => options.Configure());

        // TODO: Disable allow any origin
        const string corsPolicy = "Cors";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(corsPolicy,
                x => x.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        // Add auth0
        var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = builder.Configuration["Auth0:Audience"];
                // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("read:reservations",
                policy => policy.Requirements.Add(
                    new HasScopeRequirement("read:messages", domain)));
        });
        builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

        builder.Services.AddControllers()
            .AddOData(opt => opt.Count()
                .Filter()
                .Expand()
                .Select()
                .OrderBy()
                .AddRouteComponents(DbContextExtensions.GetModel<PmsDbContext>()))
            .AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(Registrar)));
            });

        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        // Add Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo { Title = "PMS.Backend.Service", Version = "v1" });

            c.AddXmlDocs();
            c.SupportNonNullableReferenceTypes();
        });

        // Adds FluentValidationRules staff to Swagger.
        builder.Services.AddFluentValidationRulesToSwagger();


        // Add Database
        builder.Services.AddDbContext<PmsDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("PMS")!,
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            options.UseDetached();
        });

        // Add features
        builder.Services.AddAPI();

        var app = builder.Build();

        app.UseProblemDetails();
        app.UsePathBase(new PathString("/api"));

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors(corsPolicy);
        app.MapControllers();
        app.Run();
    }
}
