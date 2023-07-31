// -----------------------------------------------------------------------
// <copyright file="AgencyContactForCreateAgencyWithContactsInput.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using HotChocolate.Types;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.GraphQL.Agency.Mutations;

namespace PMS.Backend.Features.GraphQL.Agency.Models.Input;

/// <summary>
///     The <see cref="AgencyContact"/> input for the <see cref="CreateAgencyWithContactsMutation"/>.
/// </summary>
public record CreateAgencyWithContactsAgencyContactInput
{
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
    [DefaultValue(false)]
    public bool IsFrequentVendor { get; init; }
}
