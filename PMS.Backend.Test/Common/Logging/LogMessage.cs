// -----------------------------------------------------------------------
// <copyright file="LogMessage.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Microsoft.Extensions.Logging;

namespace PMS.Backend.Test.Common.Logging;

/// <summary>
///     A log message.
/// </summary>
public class LogMessage
{
    /// <summary>
    ///     Gets or sets the log  level.
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    ///     Gets or sets the event id.
    /// </summary>
    public EventId EventId { get; set; }

    /// <summary>
    ///     Gets or sets the state.
    /// </summary>
    public object? State { get; set; }

    /// <summary>
    ///     Gets or sets the exception.
    /// </summary>
    public Exception? Exception { get; set; }

    /// <summary>
    ///     Gets or sets the message.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
