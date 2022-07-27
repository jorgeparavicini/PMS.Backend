using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Detached.Mappers.EntityFramework;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PMS.Backend.Core.Database;
using PMS.Backend.Features;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Service.Extensions;

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

        const string corsPolicy = "Cors";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(corsPolicy,
                x => x.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        builder.Services.AddControllers()
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

        builder.Services.AddAutoMapper(typeof(Registrar).Assembly);
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
        app.UseAuthorization();
        app.UseRouting();
        app.UseCors(corsPolicy);
        app.MapControllers();
        app.Run();
    }
}
