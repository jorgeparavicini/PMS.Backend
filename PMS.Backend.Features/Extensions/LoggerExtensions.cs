// -----------------------------------------------------------------------
// <copyright file="LoggerExtensions.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace PMS.Backend.Features.Extensions;

/// <summary>
///     Extensions for <see cref="ILogger"/>.
/// </summary>
public static partial class LoggerExtensions
{
    /// <summary>
    ///    Logs a message that the query is being executed.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="queryName">The name of the query.</param>
    [LoggerMessage(
        eventId: 10001,
        LogLevel.Information,
        message: "Executing query {QueryName}",
        EventName = nameof(ExecutingQuery))]
    public static partial void ExecutingQuery(this ILogger logger, string queryName);

    /// <summary>
    ///     Logs a message that the mutation is being executed.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="mutationName">The name of the mutation.</param>
    [LoggerMessage(
        eventId: 10002,
        LogLevel.Information,
        message: "Executing mutation {MutationName}",
        EventName = nameof(ExecutingMutation))]
    public static partial void ExecutingMutation(this ILogger logger, string mutationName);
}
