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
using PMS.Backend.Core.Domain.Models;
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

    public static IEnumerable<Agency> DeterministicAgencies =>
        new[]
        {
            new Agency
            {
                LegalName = "Test Agency",
                CommissionMethod = CommissionMethod.DeductedByProvider,
                DefaultCommissionRate = 0.2m,
                DefaultCommissionOnExtras = 0.1m,
                EmergencyEmail = "mail1",
                EmergencyPhone = "phone1",
                AgencyContacts = new List<AgencyContact>
                {
                    new()
                    {
                        ContactName = "Contact 1",
                        Address = "Address 1",
                        City = "City 1",
                        Email = "Email 1",
                        Phone = "Phone 1",
                        ZipCode = "Zip Code 1",
                        IsFrequentVendor = true,
                        GroupReservations = new List<GroupReservation>(),
                        Agency = null!,
                    },
                    new()
                    {
                        ContactName = "Contact 2",
                        Address = "Address 2",
                        City = "City 2",
                        Email = "Email 2",
                        Phone = "Phone 2",
                        ZipCode = "Zip Code 2",
                        IsFrequentVendor = false,
                        GroupReservations = new List<GroupReservation>(),
                        Agency = null!,
                    },
                },
            },
            new Agency
            {
                LegalName = "Test Agency 2",
                CommissionMethod = CommissionMethod.DeductedByProvider,
                DefaultCommissionRate = 0.2m,
                DefaultCommissionOnExtras = 0.1m,
                EmergencyEmail = "mail2",
                EmergencyPhone = "phone2",
                AgencyContacts = new List<AgencyContact>
                {
                    new()
                    {
                        ContactName = "Contact 3",
                        Address = "Address 3",
                        City = "City 3",
                        Email = "Email 3",
                        Phone = "Phone 3",
                        ZipCode = "Zip Code 3",
                        IsFrequentVendor = true,
                        GroupReservations = new List<GroupReservation>(),
                        Agency = null!,
                    },
                    new()
                    {
                        ContactName = "Contact 4",
                        Address = "Address 4",
                        City = "City 4",
                        Email = "Email 4",
                        Phone = "Phone 4",
                        ZipCode = "Zip Code 4",
                        IsFrequentVendor = false,
                        GroupReservations = new List<GroupReservation>(),
                        Agency = null!,
                    },
                    new()
                    {
                        ContactName = "Contact 5",
                        Address = "Address 5",
                        City = "City 5",
                        Email = "Email 5",
                        Phone = "Phone 5",
                        ZipCode = "Zip Code 5",
                        IsFrequentVendor = true,
                        GroupReservations = new List<GroupReservation>(),
                        Agency = null!,
                    },
                },
            },
            new Agency
            {
                LegalName = "Test Agency 3",
                CommissionMethod = CommissionMethod.DeductedByProvider,
                DefaultCommissionRate = 0.2m,
                DefaultCommissionOnExtras = 0.1m,
                EmergencyEmail = "mail3",
                EmergencyPhone = "phone3",
                AgencyContacts = new List<AgencyContact>
                {
                    new()
                    {
                        ContactName = "Contact 6",
                        Address = "Address 6",
                        City = "City 6",
                        Email = "Email 6",
                        Phone = "Phone 6",
                        ZipCode = "Zip Code 6",
                        IsFrequentVendor = false,
                        GroupReservations = new List<GroupReservation>(),
                        Agency = null!,
                    },
                },
            },
        };

    public static Agency CreateAgency() => Fixture.Create<Agency>();

    public static AgencyContact CreateAgencyContact() => Fixture.Create<AgencyContact>();

    public static IEnumerable<Agency> CreateAgencies(int count = 10) => Fixture.CreateMany<Agency>(count);

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
