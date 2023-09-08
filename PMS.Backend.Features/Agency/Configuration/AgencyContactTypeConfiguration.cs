// -----------------------------------------------------------------------
// <copyright file="AgencyContactTypeConfiguration.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Features.Agency.Entities;
using PMS.Backend.Features.Constants;
using PMS.Backend.Features.Shared.Configuration;

namespace PMS.Backend.Features.Agency.Configuration;

internal class AgencyContactTypeConfiguration : IEntityTypeConfiguration<AgencyContact>
{
    public void Configure(EntityTypeBuilder<AgencyContact> builder)
    {
        builder.HasKey(contact => contact.Id);

        builder.OwnsOne(contact => contact.Name,
            name =>
                name.Property(n => n.Value)
                    .HasMaxLength(StringLengths.FullName)
                    .IsRequired());

        builder.OwnsOne(contact => contact.ContactDetails,
            contactDetails =>
            {
                contactDetails.OwnsOne(contact => contact.Email,
                    email => { email.Property(e => e.Value).HasMaxLength(StringLengths.Email); });

                contactDetails.OwnsOne(contact => contact.Phone,
                    phone => { phone.Property(p => p.Value).HasMaxLength(StringLengths.PhoneNumber); });
            });

        builder.ConfigureAddress(contact => contact.Address);
    }
}
