// -----------------------------------------------------------------------
// <copyright file="AgencyContactDTO.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using PMS.Backend.Features.Features.Reservation.Models;

namespace PMS.Backend.Features.Features.Agency.Models;

/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact"/>
public record AgencyContactDTO
{
    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Id"/>
    public required int Id { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.ContactName"/>
    public required string ContactName { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Email"/>
    public string? Email { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Phone"/>
    public string? Phone { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Address"/>
    public string? Address { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.City"/>
    public string? City { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.ZipCode"/>
    public string? ZipCode { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.IsFrequentVendor"/>
    public bool IsFrequentVendor { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.AgencyId"/>
    public int AgencyId { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Agency"/>
    public required AgencyDTO Agency { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.GroupReservations"/>
    public required IList<GroupReservationDTO> GroupReservations { get; set; }
}
