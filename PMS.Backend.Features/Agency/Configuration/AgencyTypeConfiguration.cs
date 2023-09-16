// -----------------------------------------------------------------------
// <copyright file="AgencyTypeConfiguration.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Features.Constants;
using PMS.Backend.Features.Shared.Configuration;

namespace PMS.Backend.Features.Agency.Configuration;

internal class AgencyTypeConfiguration : IEntityTypeConfiguration<Entities.Agency>
{
    public void Configure(EntityTypeBuilder<Entities.Agency> builder)
    {
        builder.HasKey(agency => agency.Id);

        builder.ConfigureRequiredString(agency => agency.LegalName, StringLengths.FullName);
        builder.ConfigureCommissionMethod(agency => agency.CommissionMethod);
        builder.ConfigureCommission(agency => agency.DefaultCommission);
        builder.ConfigureCommission(agency => agency.DefaultCommissionOnExtras);
        builder.ConfigureContactDetails(agency => agency.EmergencyContact);

        builder
            .HasMany(agency => agency.Contacts)
            .WithOne()
            .HasForeignKey("AgencyId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
