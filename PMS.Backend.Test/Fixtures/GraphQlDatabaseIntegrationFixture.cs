// -----------------------------------------------------------------------
// <copyright file="GraphQlDatabaseIntegrationFixture.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Threading.Tasks;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Core.Database;
using PMS.Backend.Test.Factories;
using Testcontainers.MsSql;
using Xunit;

namespace PMS.Backend.Test.Fixtures;

public class GraphQlDatabaseIntegrationFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    private PmsWebApplicationFactory? _applicationFactory;
    private HttpClient? _httpClient;
    private RequestExecutorProxy? _executor;
    private PmsDbContext? _dbContext;

    protected PmsWebApplicationFactory ApplicationFactory => _applicationFactory ??
                                                             throw new InvalidOperationException(
                                                                 "ApplicationFactory is not initialized");

    protected HttpClient HttpClient =>
        _httpClient ?? throw new InvalidOperationException("HttpClient is not initialized");

    protected RequestExecutorProxy Executor => _executor ??
                                               throw new InvalidOperationException("Executor is not initialized");

    protected PmsDbContext DbContext =>
        _dbContext ?? throw new InvalidOperationException("DbContext is not initialized");

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();

        _applicationFactory = new PmsWebApplicationFactory(_msSqlContainer.GetConnectionString());
        _httpClient = _applicationFactory.CreateClient();

        await InitializeDatabase();

        InitializeExecutor();
    }

    public async Task DisposeAsync()
    {
        _httpClient?.Dispose();
        await _msSqlContainer.DisposeAsync();
    }

    protected async Task<HttpResponseMessage> SendQueryAsync(string query)
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/graphql")
        {
            Content = new StringContent(GetGraphQlOverHttpPostBody(query)),
        };

        return await HttpClient.SendAsync(httpRequestMessage);
    }

    protected virtual Task SeedDatabase()
    {
        return Task.CompletedTask;
    }

    private static string GetGraphQlOverHttpPostBody(string query)
    {
        return query.Replace("\n", string.Empty).Replace("\r", string.Empty);
    }

    private async Task InitializeDatabase()
    {
        using IServiceScope scope = ApplicationFactory.Services.CreateScope();
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<PmsDbContext>>();
        _dbContext = await dbContextFactory.CreateDbContextAsync();

        await DbContext.Database.EnsureCreatedAsync();
        await SeedDatabase();
    }

    private void InitializeExecutor()
    {
        using IServiceScope scope = ApplicationFactory.Services.CreateScope();
        _executor = scope.ServiceProvider.GetRequiredService<RequestExecutorProxy>();
    }
}
