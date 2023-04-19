// -----------------------------------------------------------------------
// <copyright file="AgencyContactPayload.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using PMS.Backend.Features.Features.Reservation.Models;

namespace PMS.Backend.Features.Features.Agency.Models.Payload;

/// <summary>
/// Payload for <see cref="PMS.Backend.Core.Entities.Agency.AgencyContact"/> related queries.
/// </summary>
public record AgencyContactPayload
{
    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Id"/>
    public required int Id { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.ContactName"/>
    public required string ContactName { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Email"/>
    public string? Email { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Phone"/>
    public string? Phone { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Address"/>
    public string? Address { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.City"/>
    public string? City { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.ZipCode"/>
    public string? ZipCode { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.IsFrequentVendor"/>
    public bool IsFrequentVendor { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.AgencyId"/>
    public int AgencyId { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Agency"/>
    public required AgencyPayload Agency { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.GroupReservations"/>
    public required IList<GroupReservationDTO> GroupReservations { get; init; }
}
