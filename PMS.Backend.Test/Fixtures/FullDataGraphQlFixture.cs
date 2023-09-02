// -----------------------------------------------------------------------
// <copyright file="FullDataGraphQlFixture.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Test.Builders.Agency.Entity;
using PMS.Backend.Test.Builders.Reservation.Entity;

namespace PMS.Backend.Test.Fixtures;

[UsedImplicitly]
public class FullDataGraphQlFixture : GraphQlFixture
{
    protected override async Task SeedDatabase()
    {
        IList<Core.Entities.Agency.Agency> agencies = Agencies();
        DbContext.Agencies.AddRange(agencies);

        IList<GroupReservation> reservations = Reservations(agencies);
        DbContext.GroupReservations.AddRange(reservations);
        await DbContext.SaveChangesAsync();
    }

    private static IList<Core.Entities.Agency.Agency> Agencies() => new[]
    {
        new AgencyBuilder().WithLegalName("Agency 1")
            .AddAgencyContacts(builder => builder.WithContactName("Contact 1"), _ => { })
            .Build(),
        new AgencyBuilder().WithLegalName("Agency 2").AddAgencyContacts(_ => { }).Build(),
    };

    private static IList<GroupReservation> Reservations(IList<Core.Entities.Agency.Agency> agencies) =>
        new[]
        {
            new GroupReservationBuilder()
                .WithAgencyContactId(agencies.First().AgencyContacts.First().Id)
                .WithReservations(builder => builder
                    .WithReservationDetails(_ => { }))
                .Build(),
            new GroupReservationBuilder()
                .WithAgencyContactId(agencies.First().AgencyContacts[1].Id)
                .WithReservations(
                    reservations => reservations
                        .WithName("Family 1")
                        .WithReservationDetails(details => details
                            .WithCheckIn(DateOnly.FromDateTime(DateTime.Today.AddDays(5)))
                            .WithCheckOut(DateOnly.FromDateTime(DateTime.Today.AddDays(9)))),
                    reservation => reservation
                        .WithName("Family 2")
                        .WithReservationDetails(_ => { }, _ => { }))
                .Build(),
        };
}
