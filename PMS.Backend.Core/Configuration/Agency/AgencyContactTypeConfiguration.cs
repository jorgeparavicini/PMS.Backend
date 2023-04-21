// -----------------------------------------------------------------------
// <copyright file="AgencyContactTypeConfiguration.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Core.Entities.Agency;

namespace PMS.Backend.Core.Configuration.Agency;

public class AgencyContactTypeConfiguration : EntityTypeConfiguration<AgencyContact>
{
    public override void Configure(EntityTypeBuilder<AgencyContact> builder)
    {
        base.Configure(builder);

        builder.Property(agencyContact => agencyContact.ContactName).IsRequired().HasMaxLength(255);

        builder.Property(agencyContact => agencyContact.Email).HasMaxLength(255);

        builder.Property(agencyContact => agencyContact.Phone).HasMaxLength(255);

        builder.Property(agencyContact => agencyContact.Address).HasMaxLength(255);

        builder.Property(agencyContact => agencyContact.City).HasMaxLength(255);

        builder.Property(agencyContact => agencyContact.ZipCode).HasMaxLength(255);

        builder.Property(agencyContact => agencyContact.IsFrequentVendor).HasDefaultValue(false);
    }
}
