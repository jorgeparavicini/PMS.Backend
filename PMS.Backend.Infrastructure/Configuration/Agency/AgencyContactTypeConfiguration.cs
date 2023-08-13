// -----------------------------------------------------------------------
// <copyright file="AgencyContactTypeConfiguration.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Common.Constants;
using PMS.Backend.Domain.Aggregates.Agency;

namespace PMS.Backend.Persistence.Configuration.Agency;

public class AgencyContactTypeConfiguration : IEntityTypeConfiguration<AgencyContact>
{
    public void Configure(EntityTypeBuilder<AgencyContact> builder)
    {
        builder.HasKey(contact => contact.Id);

        builder.Property(contact => contact.Name).IsRequired().HasMaxLength(StringLengths.FullName);
        builder.Property(contact => contact.ContactDetails).IsRequired();
        builder.Property(contact => contact.Address).IsRequired();
    }
}
