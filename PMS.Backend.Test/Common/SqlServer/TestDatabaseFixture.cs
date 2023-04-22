// -----------------------------------------------------------------------
// <copyright file="TestDatabaseFixture.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Detached.Mappers.EntityFramework;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Test.Data;
using Testcontainers.MsSql;
using Xunit;

namespace PMS.Backend.Test.Common.SqlServer;

public abstract class TestDatabaseFixture : IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    private PmsDbContext? _dbContext;

    protected string ConnectionString => _msSqlContainer.GetConnectionString();

    protected PmsDbContext DbContext =>
        _dbContext ??= new(new DbContextOptionsBuilder<PmsDbContext>()
            .UseSqlServer(ConnectionString)
            .UseDetached()
            .Options);

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
        await DbContext.Database.EnsureCreatedAsync();
        await SeedDatabase();
    }

    public async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync();
    }

    private async Task SeedDatabase()
    {
        IEnumerable<Agency> entities = AgencyData.GetMultiple(5);
        DbContext.Agencies.AddRange(entities);
        await DbContext.SaveChangesAsync();
    }
}
