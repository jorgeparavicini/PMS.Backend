// -----------------------------------------------------------------------
// <copyright file="MsSqlContainerFixture.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NuGet.Common;
using Testcontainers.MsSql;

namespace PMS.Backend.Test.Fixtures;

/// <summary>
///     A global fixture that starts a new MsSql container if needed and allows to create new databases.
/// </summary>
public partial class MsSqlContainerFixture : IAsyncDisposable
{
    private static readonly AsyncLazy<MsSqlContainerFixture> AsyncLazyInstance =
        new(async () =>
        {
            MsSqlContainerFixture container = new();
            await container.InitializeAsync();
            return container;
        });

    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    private MsSqlContainerFixture()
    {
    }

    private string MasterDbConnectionString => _msSqlContainer.GetConnectionString();

    public static async Task<MsSqlContainerFixture> GetInstance() => await AsyncLazyInstance;

    public string CreateNewConnectionString()
    {
        string connectionString = MasterDbConnectionString;
        string databaseName = CreateRandomDatabaseName();

        return DatabaseNamePattern().Replace(connectionString, databaseName);
    }

    public async ValueTask DisposeAsync()
    {
        await _msSqlContainer.StopAsync();

        GC.SuppressFinalize(this);
    }

    private static string CreateRandomDatabaseName()
    {
        return Guid.NewGuid().ToString("N");
    }

    [GeneratedRegex("(?<=Database=)([^;]+)")]
    private static partial Regex DatabaseNamePattern();

    private async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
    }
}
