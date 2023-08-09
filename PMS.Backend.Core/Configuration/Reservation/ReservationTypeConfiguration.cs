// -----------------------------------------------------------------------
// <copyright file="ReservationTypeConfiguration.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PMS.Backend.Core.Configuration.Reservation;

/// <summary>
///     The ef core configuration for the <see cref="Entities.Reservation.Reservation" /> entity.
/// </summary>
public class ReservationTypeConfiguration : EntityTypeConfiguration<Entities.Reservation.Reservation>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<Entities.Reservation.Reservation> builder)
    {
        base.Configure(builder);

        builder.Property(reservation => reservation.Name).HasMaxLength(255);
    }
}
