// -----------------------------------------------------------------------
// <copyright file="AgencyGraphQlFixture.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using PMS.Backend.Test.Builders.Agency.Entity;

namespace PMS.Backend.Test.Fixtures.Agency;

[UsedImplicitly]
public class AgencyGraphQlFixture : GraphQlFixture
{
    protected IEnumerable<Core.Entities.Agency.Agency> Entities { get; } = new[]
    {
        new AgencyBuilder().WithLegalName("Agency 1")
            .AddAgencyContacts(
                builder => builder.WithContactName("Contact 1"),
                _ => { })
            .Build(),
        new AgencyBuilder().WithLegalName("Agency 2").AddAgencyContacts(_ => { }).Build(),
    };

    protected override async Task SeedDatabase()
    {
        DbContext.Agencies.AddRange(Entities);
        await DbContext.SaveChangesAsync();
    }
}
