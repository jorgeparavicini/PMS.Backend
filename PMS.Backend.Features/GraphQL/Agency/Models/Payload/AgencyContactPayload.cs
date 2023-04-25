// -----------------------------------------------------------------------
// <copyright file="AgencyContactPayload.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PMS.Backend.Core.Entities.Agency;

namespace PMS.Backend.Features.GraphQL.Agency.Models.Payload;

/// <inheritdoc cref="AgencyContact"/>
public record AgencyContactPayload
{
    /// <inheritdoc cref="AgencyContact.Id"/>
    public required int Id { get; init; }

    /// <inheritdoc cref="AgencyContact.ContactName"/>
    public required string ContactName { get; init; }

    /// <inheritdoc cref="AgencyContact.Email"/>
    public string? Email { get; init; }

    /// <inheritdoc cref="AgencyContact.Phone"/>
    public string? Phone { get; init; }

    /// <inheritdoc cref="AgencyContact.Address"/>
    public string? Address { get; init; }

    /// <inheritdoc cref="AgencyContact.City"/>
    public string? City { get; init; }

    /// <inheritdoc cref="AgencyContact.ZipCode"/>
    public string? ZipCode { get; init; }

    /// <inheritdoc cref="AgencyContact.IsFrequentVendor"/>
    public bool IsFrequentVendor { get; init; }

    /// <inheritdoc cref="AgencyContact.AgencyId"/>
    public int AgencyId { get; init; }

    /// <inheritdoc cref="AgencyContact.Agency"/>
    public required AgencyPayload Agency { get; init; }

    // <inheritdoc cref="AgencyContact.GroupReservations"/>
    // TODO: Change to correct type once implemented.
    // public required IList<GroupReservationDTO> GroupReservations { get; init; }
}
