// -----------------------------------------------------------------------
// <copyright file="AgencyContact.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using PMS.Backend.Core.Entities.Reservation;

namespace PMS.Backend.Core.Entities.Agency;

/// <summary>
/// The contact of an agent part of an agency.
/// </summary>
public class AgencyContact : Entity
{
    /// <summary>
    /// Gets or sets the full name of the contact.
    /// </summary>
    public required string ContactName { get; set; }

    /// <summary>
    /// Gets or sets an optional email address for the contact.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets an optional phone number for the contact.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets an optional address for the contact.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets an optional city of residence for the contact.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets an optional ZipCode where the contact resides.
    /// </summary>
    public string? ZipCode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether is the contact an agent that provides frequent sells.
    /// </summary>
    public bool IsFrequentVendor { get; set; }

    /// <summary>
    /// Gets or sets the id of the associated agency.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the agency are required.
    /// </remarks>
    /// <seealso cref="Agency"/>
    public int AgencyId { get; set; }

    /// <summary>
    /// Gets or sets the associated agency.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the agency are required.
    /// </remarks>
    /// <seealso cref="AgencyId"/>
    public required Agency Agency { get; set; }

    /// <summary>
    /// Gets or sets a list of all reservations this contact is responsible for.
    /// </summary>
    public required IList<GroupReservation> GroupReservations { get; set; }

    // TODO: Add Country of residence, Language
}
