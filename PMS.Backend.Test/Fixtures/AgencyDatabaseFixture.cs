// -----------------------------------------------------------------------
// <copyright file="AgencyDatabaseFixture.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Test.Data;

namespace PMS.Backend.Test.Fixtures;

public class AgencyDatabaseFixture : DatabaseFixture
{
    protected static int AgencyCount => 5;

    protected override async Task SeedDatabase()
    {
        IEnumerable<Agency> entities = AgencyData.CreateAgencies(AgencyCount);
        DbContext.Agencies.AddRange(entities);
        await DbContext.SaveChangesAsync();
    }
}
