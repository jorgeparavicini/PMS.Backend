// -----------------------------------------------------------------------
// <copyright file="AgencyTypeConfiguration.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Common.Constants;
using PMS.Backend.Domain.ValueObjects;

namespace PMS.Backend.Persistence.Configuration.Agency;

public class AgencyTypeConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Agency.Agency>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Agency.Agency> builder)
    {
        builder.HasKey(agency => agency.Id);

        builder.Property(agency => agency.LegalName).IsRequired().HasMaxLength(StringLengths.FullName);

        builder.Property(agency => agency.CommissionMethod)
            .HasConversion(method => method.Id, id => CommissionMethod.From(id))
            .IsRequired();

        builder.OwnsOne(agency => agency.DefaultCommission);
        builder.OwnsOne(agency => agency.DefaultCommissionOnExtras);
        builder.OwnsOne(agency => agency.EmergencyContact);

        builder
            .HasMany(agency => agency.AgencyContacts)
            .WithOne()
            .HasForeignKey("AgencyId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
