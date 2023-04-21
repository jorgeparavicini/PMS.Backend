// -----------------------------------------------------------------------
// <copyright file="Agency.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Detached.Annotations;
using PMS.Backend.Common.Models;

namespace PMS.Backend.Core.Entities.Agency;

/// <summary>
/// An agency through which guests can make reservations.
/// </summary>
public class Agency : Entity
{
    /// <summary>
    /// Gets or sets the legal name of the agency.
    /// </summary>
    public required string LegalName { get; set; }

    /// <summary>
    /// Gets or sets the default commission rate for agents in this agency as a fraction.
    /// <para>
    /// If the agent has not specified a commission rate, this default rate will be used.
    /// </para>
    /// </summary>
    /// <remarks>
    /// Max value is 9.9999 but semantically it does not make sense to have it more than 1.0.
    /// <para>Min value is 0.0000.</para>
    /// </remarks>
    public decimal? DefaultCommissionRate { get; set; }

    /// <summary>
    /// Gets or sets the default commission rate for extra goods provided for agents in this agency as a
    /// fraction.
    /// <para>
    /// If the agent has not specified a commission rate for extra good, this default rate will be
    /// used.
    /// </para>
    /// </summary>
    /// <remarks>
    /// Max value is 9.9999 but semantically it does not make sense to have it more than 1.0.
    /// <para>Min value is 0.0000.</para>
    /// </remarks>
    public decimal? DefaultCommissionOnExtras { get; set; }

    /// <summary>
    /// Gets or sets the method to be used for commissions.
    /// </summary>
    public CommissionMethod CommissionMethod { get; set; }

    /// <summary>
    /// Gets or sets an optional phone number in case of emergencies.
    /// </summary>
    public string? EmergencyPhone { get; set; }

    /// <summary>
    /// Gets or sets an optional email address in case of emergencies.
    /// </summary>
    public string? EmergencyEmail { get; set; }

    /// <summary>
    /// Gets or sets the agents known that work for this agency.
    /// </summary>
    /// <remarks>
    /// At least one contact is required as relations are established through the contact
    /// and not the agency.
    /// </remarks>
    [Composition]
    public required IList<AgencyContact> AgencyContacts { get; set; }

    // TODO: Add Company, Association, Default Channel, Default Origination
}
