// -----------------------------------------------------------------------
// <copyright file="GroupReservationTypeConfiguration.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Core.Entities.Reservation;

namespace PMS.Backend.Core.Configuration.Reservation;

/// <summary>
///     The ef core configuration for the <see cref="GroupReservation" /> entity.
/// </summary>
public class GroupReservationTypeConfiguration : EntityTypeConfiguration<GroupReservation>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<GroupReservation> builder)
    {
        base.Configure(builder);

        builder.Property(reservation => reservation.Reference).HasMaxLength(255);
    }
}
