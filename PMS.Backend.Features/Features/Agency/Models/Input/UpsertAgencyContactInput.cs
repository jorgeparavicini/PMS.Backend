// -----------------------------------------------------------------------
// <copyright file="UpsertAgencyContactInput.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using HotChocolate.Types;
using PMS.Backend.Features.Features.Agency.Mutations;

namespace PMS.Backend.Features.Features.Agency.Models.Input;

/// <summary>
/// Input data for the <see cref="UpsertAgencyContactMutation"/>.
/// </summary>
public class UpsertAgencyContactInput
{
    /// <summary>
    /// Gets the id of the associated <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> entity.
    /// </summary>
    public required int AgencyId { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Id"/>
    public int? Id { get; init; }

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
    [DefaultValue(false)]
    public bool IsFrequentVendor { get; init; }
}
