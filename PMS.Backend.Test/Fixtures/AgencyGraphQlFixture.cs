// -----------------------------------------------------------------------
// <copyright file="AgencyGraphQlFixture.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using JetBrains.Annotations;
using PMS.Backend.Test.Data;

namespace PMS.Backend.Test.Fixtures;

[UsedImplicitly]
public class AgencyGraphQlFixture : GraphQlFixture
{
    protected override async Task SeedDatabase()
    {
        DbContext.Agencies.AddRange(AgencyData.DeterministicAgencies);
        await DbContext.SaveChangesAsync();
    }
}
