// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Detached.Mappers.EntityFramework;
using FluentValidation;
using HotChocolate.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PMS.Backend.Core.Database;
using PMS.Backend.Features;
using PMS.Backend.Features.GraphQL;
using PMS.Backend.Service.Security;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// TODO: Use Configuration.Get<Type> Syntax
string env = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile($"appsettings.{env}.json", true, true);

// Cors
const string corsPolicy = "Cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        corsPolicy,
        x => x.WithOrigins(builder.Configuration["CorsOrigin"]!)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

// Add auth0
var domain = $"https://{builder.Configuration["Auth0:Domain"]}";
string? audience = builder.Configuration["Auth0:Audience"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = domain;
        options.Audience = audience;

        // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier,
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Services.AddValidatorsFromAssembly(typeof(Registrar).Assembly);
builder.Services.AddRouting(options => options.LowercaseUrls = true);

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
