// -----------------------------------------------------------------------
// <copyright file="AgencyTypeConfiguration.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Common.Models;
using PMS.Backend.Core.Entities.Agency;

namespace PMS.Backend.Core.Configuration.Agency;

public class AgencyTypeConfiguration : EntityTypeConfiguration<Entities.Agency.Agency>
{
    public override void Configure(EntityTypeBuilder<Entities.Agency.Agency> builder)
    {
        base.Configure(builder);

        builder.Property(agency => agency.LegalName).IsRequired().HasMaxLength(255);

        builder.Property(agency => agency.DefaultCommissionRate).HasPrecision(5, 4);

        builder.Property(agency => agency.DefaultCommissionOnExtras).HasPrecision(5, 4);

        builder.Property(agency => agency.CommissionMethod).HasDefaultValue(CommissionMethod.DeductedByAgency);

        builder.Property(agency => agency.EmergencyPhone).HasMaxLength(255);

        builder.Property(agency => agency.EmergencyEmail).HasMaxLength(255);

        builder
            .HasMany<AgencyContact>(agency => agency.AgencyContacts)
            .WithOne(agencyContact => agencyContact.Agency)
            .HasForeignKey(agencyContact => agencyContact.AgencyId)
            .OnDelete(DeleteBehavior.ClientCascade);
    }
}
