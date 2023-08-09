// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using PMS.Backend.Service.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentConfiguration(builder.Environment)
    .AddUserSecrets<Program>(optional: true, reloadOnChange: true);

builder.Services
    .AddValidators()
    .AddEfCore(builder.Configuration)
    .AddAutoMapper()
    .AddApplicationCors()
    .AddGraphQl(builder.Environment);

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.UseCors();
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
