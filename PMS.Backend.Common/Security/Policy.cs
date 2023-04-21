// -----------------------------------------------------------------------
// <copyright file="Policy.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace PMS.Backend.Common.Security;

/// <summary>
/// Auth0 Policies.
/// </summary>
public enum Policy
{
    /// <summary>
    /// Allows reading reservations.
    /// </summary>
    ReadReservations,

    /// <summary>
    /// Allows creating reservations.
    /// </summary>
    CreateReservations,

    /// <summary>
    /// Allows updating reservations.
    /// </summary>
    UpdateReservations,

    /// <summary>
    /// Allows deleting reservations
    /// </summary>
    DeleteReservations,

    /// <summary>
    /// Allows reading agencies.
    /// </summary>
    ReadAgencies,

    /// <summary>
    /// Allows creating agencies.
    /// </summary>
    CreateAgencies,

    /// <summary>
    /// Allows updating agencies.
    /// </summary>
    UpdateAgencies,

    /// <summary>
    /// Allows deleting agencies.
    /// </summary>
    DeleteAgencies,
}
