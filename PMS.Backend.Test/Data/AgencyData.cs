// -----------------------------------------------------------------------
// <copyright file="AgencyData.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using AutoFixture;
using AutoFixture.Dsl;
using AutoFixture.Kernel;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Test.Common.Customization;

namespace PMS.Backend.Test.Data;

public static class AgencyData
{
    private static readonly Fixture Fixture;

    static AgencyData()
    {
        Fixture = new Fixture();
        Fixture.Customize(new CompositeCustomization(
            new EntityCustomization(),
            new CommissionCustomization<Agency>(
                agency => agency.DefaultCommissionRate,
                agency => agency.DefaultCommissionOnExtras)));
        Fixture.Customize<Agency>(
            composer => composer.WithoutAggregateRelations(Fixture));
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    public static Agency GetSingle() => Fixture.Create<Agency>();

    public static IEnumerable<Agency> GetMultiple(int count = 10) => Fixture.CreateMany<Agency>(count);

    private static IPostprocessComposer<Agency> WithoutAggregateRelations(
        this IPostprocessComposer<Agency> composer,
        ISpecimenBuilder fixture)
    {
        return composer.With(
            agency => agency.AgencyContacts,
            () =>
            {
                var agencyContacts = fixture.Create<List<AgencyContact>>();
                agencyContacts.ForEach(agencyContact => agencyContact.GroupReservations = new List<GroupReservation>());
                return agencyContacts;
            });
    }
}
