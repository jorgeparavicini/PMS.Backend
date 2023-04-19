// -----------------------------------------------------------------------
// <copyright file="UpsertAgencyContactPayload.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PMS.Backend.Features.Features.Agency.Mutations;

namespace PMS.Backend.Features.Features.Agency.Models.Payload;

/// <summary>
/// The payload for the <see cref="UpsertAgencyContactMutation"/>.
/// </summary>
public record UpsertAgencyContactPayload
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
}
