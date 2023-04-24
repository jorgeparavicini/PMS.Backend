// -----------------------------------------------------------------------
// <copyright file="PmsWebApplicationFactory.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace PMS.Backend.Test.Factories;

public class PmsWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public PmsWebApplicationFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(serviceProvider => new RequestExecutorProxy(
                serviceProvider.GetRequiredService<IRequestExecutorResolver>(),
                Schema.DefaultName));
        });

        builder.ConfigureAppConfiguration(config =>
        {
            config.Sources.Add(new MemoryConfigurationSource()
            {
                InitialData = new[]
                {
                    new KeyValuePair<string, string?>("ConnectionStrings:PMS", _connectionString),
                },
            });
        });

        builder.UseEnvironment("Integration");
    }
}
