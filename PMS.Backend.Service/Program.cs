// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Security.Claims;
using AutoMapper;
using Detached.Mappers.EntityFramework;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using HotChocolate.Data;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using PMS.Backend.Common.Extensions;
using PMS.Backend.Common.Security;
using PMS.Backend.Core.Database;
using PMS.Backend.Features;
using PMS.Backend.Features.Features.Agency;
using PMS.Backend.Features.Features.Reservation;
using PMS.Backend.Features.GraphQL;
using PMS.Backend.Service.Extensions;
using PMS.Backend.Service.Filters;
using PMS.Backend.Service.Security;
using DbContextExtensions = PMS.Backend.Service.Extensions.DbContextExtensions;

var builder = WebApplication.CreateBuilder(args);

// TODO: Use Configuration.Get<Type> Syntax
var env = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile($"appsettings.{env}.json", true, true);

// Add exception handling middleware
builder.Services.AddProblemDetails(options => options.Configure(env));

// Cors
const string corsPolicy = "Cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy,
        x => x.WithOrigins(builder.Configuration["CorsOrigin"])
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    );
});

// Add auth0
var domain = $"https://{builder.Configuration["Auth0:Domain"]}";
var audience = builder.Configuration["Auth0:Audience"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = audience;

        // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });
builder.Services.AddAuthorization(options =>
{
    foreach (var policy in Enum.GetValues<Policy>())
    {
        options.AddPolicy(policy.ToString(),
            p => p.Requirements.Add(new HasScopeRequirement(policy.GetScope(), domain)));
    }
});
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Services.AddControllers(options =>
    {
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    })
    .AddOData(opt => opt.Count()
        .Filter()
        .Expand()
        .Select()
        .OrderBy()
        .AddRouteComponents(DbContextExtensions.GetModel<PmsDbContext>()))
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.Services.AddValidatorsFromAssembly(typeof(Registrar).Assembly);
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "PMS.Backend.Service", Version = "v1" });

    c.AddXmlDocs();
    c.SupportNonNullableReferenceTypes();

    c.AddSecurityScheme(domain, audience);

    c.OperationFilter<SecurityRequirementsOperationFilter>();
    c.OperationFilter<EnableQueryFilter>();
});

// Add Swagger extensions
builder.Services.AddFluentValidationRulesToSwagger();
builder.Services.AddSwaggerGenNewtonsoftSupport();

// Add Database
builder.Services.AddPooledDbContextFactory<PmsDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("PMS")!;
    options
        .UseSqlServer(
            connectionString,
            serverOptions => serverOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
        .UseDetached();
});

builder.Services.AddAutoMapper(typeof(Registrar).Assembly);

builder.Services.AddGraphQLServer()
    .AddMutationType<Mutation>()
    .AddQueryType<Query>()
    .AddFeatureTypes()
    .RegisterDbContext<PmsDbContext>(DbContextKind.Pooled)
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddFairyBread()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment());

WebApplication app = builder.Build();

app.UseProblemDetails();
app.UsePathBase(new PathString("/api"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.OAuthClientId(builder.Configuration["Auth0:ClientId"]); });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(corsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();
app.Run();

/// <summary>
/// The entry point of the program.
/// </summary>
[ExcludeFromCodeCoverage]
public partial class Program
{
}
