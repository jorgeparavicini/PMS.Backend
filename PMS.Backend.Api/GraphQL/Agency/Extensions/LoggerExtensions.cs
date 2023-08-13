// -----------------------------------------------------------------------
// <copyright file="LoggerExtensions.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Microsoft.Extensions.Logging;

namespace PMS.Backend.Api.GraphQL.Agency.Extensions;

/// <summary>
///     Extensions for <see cref="ILogger"/>.
/// </summary>
public static partial class LoggerExtensions
{
    /// <summary>
    ///     Logs the creation of an agency.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="agencyId">
    ///     The ID of the <see cref="Agency"/> entity that was created.
    /// </param>
    [LoggerMessage(
        eventId: 10101,
        LogLevel.Information,
        message: "Created agency {AgencyId}",
        EventName = nameof(AgencyCreated))]
    public static partial void AgencyCreated(this ILogger logger, Guid agencyId);

    /// <summary>
    ///     Logs the creation of an agency contact.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="agencyContactId">
    ///    The ID of the <see cref="Core.Entities.Agency.AgencyContact"/> entity that was created.
    /// </param>
    [LoggerMessage(
        eventId: 10102,
        LogLevel.Information,
        message: "Created agency contact {AgencyContactId}",
        EventName = nameof(AgencyContactCreated))]
    public static partial void AgencyContactCreated(this ILogger logger, Guid agencyContactId);

    /// <summary>
    ///     Logs the editing of an agency.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="agencyId">
    ///     The ID of the <see cref="Core.Entities.Agency.Agency"/> entity that was edited.
    /// </param>
    [LoggerMessage(
        eventId: 10103,
        LogLevel.Information,
        message: "Edited agency {AgencyId}",
        EventName = nameof(AgencyEdited))]
    public static partial void AgencyEdited(this ILogger logger, Guid agencyId);

    /// <summary>
    ///     Logs the editing of an agency contact.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="agencyContactId">
    ///     The ID of the <see cref="Core.Entities.Agency.AgencyContact"/> entity that was edited.
    /// </param>
    [LoggerMessage(
        eventId: 10104,
        LogLevel.Information,
        message: "Edited agency contact {AgencyContactId}",
        EventName = nameof(AgencyContactEdited))]
    public static partial void AgencyContactEdited(this ILogger logger, Guid agencyContactId);

    /// <summary>
    ///     Logs that an agency contact was moved to a different agency.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="agencyContactId">
    ///     The ID of the <see cref="Core.Entities.Agency.AgencyContact"/> entity that was moved.
    /// </param>
    /// <param name="agencyId">
    ///     The ID of the <see cref="Core.Entities.Agency.Agency"/> entity that the contact was moved to.
    /// </param>
    [LoggerMessage(
        eventId: 10105,
        LogLevel.Information,
        message: "Moved agency contact {AgencyContactId} to agency {AgencyId}",
        EventName = nameof(AgencyContactMovedToAgency))]
    public static partial void AgencyContactMovedToAgency(this ILogger logger, Guid agencyContactId, Guid agencyId);

    /// <summary>
    ///     Logs the deletion of an agency.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="agencyId">
    ///     The ID of the <see cref="Core.Entities.Agency.Agency"/> entity that was deleted.
    /// </param>
    [LoggerMessage(
        eventId: 10106,
        LogLevel.Information,
        message: "Deleted agency {AgencyId}",
        EventName = nameof(AgencyDeleted))]
    public static partial void AgencyDeleted(this ILogger logger, Guid agencyId);

    /// <summary>
    ///     Logs the deletion of an agency contact.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="agencyContactId">
    ///     The ID of the <see cref="Core.Entities.Agency.AgencyContact"/> entity that was deleted.
    /// </param>
    [LoggerMessage(
        eventId: 10107,
        LogLevel.Information,
        message: "Deleted agency contact {AgencyContactId}",
        EventName = nameof(AgencyContactDeleted))]
    public static partial void AgencyContactDeleted(this ILogger logger, Guid agencyContactId);

    /// <summary>
    ///     Logs that an agency contact was already assigned to an agency.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="agencyContactId">
    ///     The ID of the <see cref="Core.Entities.Agency.AgencyContact"/> entity that was already assigned.
    /// </param>
    /// <param name="agencyId">
    ///     The ID of the <see cref="Core.Entities.Agency.Agency"/> entity that the contact was already assigned to.
    /// </param>
    [LoggerMessage(
        eventId: 10108,
        LogLevel.Information,
        message: "Agency contact {AgencyContactId} is already assigned to agency {AgencyId}",
        EventName = nameof(AgencyContactIsAlreadyAssignedToAgency))]
    public static partial void AgencyContactIsAlreadyAssignedToAgency(
        this ILogger logger,
        Guid agencyContactId,
        Guid agencyId);
}
