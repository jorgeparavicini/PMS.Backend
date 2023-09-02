// -----------------------------------------------------------------------
// <copyright file="AgencyDatabaseFixture.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using PMS.Backend.Test.Builders.Agency.Entity;

namespace PMS.Backend.Test.Fixtures.Agency;

public class AgencyDatabaseFixture : DatabaseFixture
{
    protected IEnumerable<Core.Entities.Agency.Agency> Entities { get; } = new[]
    {
        new AgencyBuilder().WithLegalName("Agency 1")
            .AddAgencyContacts(builder => builder.WithContactName("Contact 1"))
            .AddAgencyContacts(_ => { })
            .Build(),
        new AgencyBuilder().WithLegalName("Agency 2").AddAgencyContacts(_ => { }).Build(),
    };

    protected override async Task SeedDatabase()
    {
        DbContext.Agencies.AddRange(Entities);
        await DbContext.SaveChangesAsync();
    }
}
