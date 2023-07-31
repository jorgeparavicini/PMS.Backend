// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using Detached.Mappers.EntityFramework;
using FluentValidation;
using HotChocolate.Data;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMS.Backend.Core.Database;
using PMS.Backend.Features;
using PMS.Backend.Features.GraphQL;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string env = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile($"appsettings.{env}.json", true, true);

builder.Services.AddValidatorsFromAssembly(typeof(Registrar).Assembly);

builder.Services.AddPooledDbContextFactory<PmsDbContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("PMS")!;
    options
        .UseSqlServer(
            connectionString,
            serverOptions => serverOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
        .UseDetached()
        .UseMapping(cfg =>
        {
            cfg.Default(opts =>
            {
                opts.Primitives.Add(typeof(DateOnly));
                opts.Primitives.Add(typeof(TimeOnly));
            });
        });
});

builder.Services.AddAutoMapper(typeof(Registrar).Assembly);
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "Cors",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

// TODO: This must obviously be changed to a more secure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "Cors",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

builder.Services.AddGraphQLServer()
    .AddMutationType<Mutation>()
    .AddQueryType<Query>()
    .AddGraphQlFeatures()
    .RegisterDbContext<PmsDbContext>(DbContextKind.Pooled)
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddFairyBread()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment());

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("Cors");
app.MapGraphQL();
app.Run();

/// <summary>
/// The entry point of the program.
/// </summary>
[ExcludeFromCodeCoverage]
[UsedImplicitly]
public partial class Program
{
}
