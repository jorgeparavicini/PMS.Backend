// -----------------------------------------------------------------------
// <copyright file="DatabaseFixture.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Detached.Mappers.EntityFramework;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Database;
using Xunit;

namespace PMS.Backend.Test.Fixtures;

public class DatabaseFixture : IAsyncLifetime
{
    private PmsDbContext? _dbContext;

    public PmsDbContext DbContext => _dbContext ?? throw new InvalidOperationException("DbContext is not initialized");

    public async Task InitializeAsync()
    {
        var msSqlContainerFixture = await MsSqlContainerFixture.GetInstance();
        DbContextOptions<PmsDbContext> options = new DbContextOptionsBuilder<PmsDbContext>()
            .UseSqlServer(msSqlContainerFixture.CreateNewConnectionString())
            .UseDetached()
            .Options;
        _dbContext = new PmsDbContext(options);
        await _dbContext.Database.EnsureCreatedAsync();
        await SeedDatabase();
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
    }

    protected virtual Task SeedDatabase()
    {
        return Task.CompletedTask;
    }
}
