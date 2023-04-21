// -----------------------------------------------------------------------
// <copyright file="RecordingLogger.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace PMS.Backend.Test.Common.Logging;

/// <summary>
///     An <see cref="ILogger{TCategoryName}"/> implementation that records all logged messages.
///     This class can be used to verify that a certain log message was logged by a class
///     that uses the <see cref="ILogger{TCategoryName}"/> interface.
/// </summary>
/// <typeparam name="TCategoryName">
///     The type that this logger is associated with.
/// </typeparam>
public class RecordingLogger<TCategoryName> : ILogger<TCategoryName>
{
    private readonly ILogger<TCategoryName>? _logger;

    private readonly IDictionary<LogLevel, IList<LogMessage>> _recordedMessages =
        new Dictionary<LogLevel, IList<LogMessage>>();

    /// <summary>
    ///     Initializes a new instance of the <see cref="RecordingLogger{TCategoryName}"/> class.
    /// </summary>
    /// <param name="logger">A logger.</param>
    [SuppressMessage(
        "ReSharper",
        "ContextualLoggerProblem",
        Justification = "This is a utility class which is used for testing.")]
    public RecordingLogger(ILogger<TCategoryName> logger)
        : this()
    {
        _logger = logger;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="RecordingLogger{TCategoryName}"/> class.
    /// </summary>
    public RecordingLogger() => InitializeRecordedMessages();

    /// <inheritdoc />
    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        LogMessage logMessage = new()
        {
            LogLevel = logLevel,
            EventId = eventId,
            State = state,
            Exception = exception,
            Message = formatter(state, exception),
        };

        _recordedMessages[logLevel].Add(logMessage);
        _logger?.Log(logLevel, eventId, state, exception, formatter);
    }

    /// <inheritdoc />
    public bool IsEnabled(LogLevel logLevel) => true;

    /// <inheritdoc />
    public IDisposable BeginScope<TState>(TState state)
        where TState : notnull => NullDisposable.Instance;

    /// <summary>
    ///     Resets the recorded messages.
    /// </summary>
    public void Reset()
    {
        _recordedMessages.Clear();
        InitializeRecordedMessages();
    }

    /// <summary>
    ///     Gets the recorded messages for the specified log level.
    /// </summary>
    /// <param name="logLevel">The log level.</param>
    /// <returns>The recorded messages.</returns>
    public IEnumerable<LogMessage> GetRecordedMessages(LogLevel logLevel)
        => _recordedMessages[logLevel];

    public void ShouldHaveLogged(Expression loggerMethodExpression)
    {
        LoggerMessageAttribute loggerMessageAttribute = GetLogMessageAttribute(loggerMethodExpression);
        GetRecordedMessages(loggerMessageAttribute.Level)
            .ToList()
            .Should()
            .Contain(
                (Expression<Func<LogMessage, bool>>)(m =>
                    m.EventId.Id == loggerMessageAttribute.EventId &&
                    m.EventId.Name == loggerMessageAttribute.EventName),
                string.Empty);
    }

    public void ShouldNotHaveLogged(Expression loggerMethodExpression)
    {
        LoggerMessageAttribute loggerMessageAttribute = GetLogMessageAttribute(loggerMethodExpression);
        GetRecordedMessages(loggerMessageAttribute.Level)
            .ToList()
            .Should()
            .NotContain(
                (Expression<Func<LogMessage, bool>>)(m =>
                    m.EventId.Id == loggerMessageAttribute.EventId &&
                    m.EventId.Name == loggerMessageAttribute.EventName),
                string.Empty);
    }

    private static LoggerMessageAttribute GetLogMessageAttribute(Expression expression)
    {
        if (expression.NodeType != ExpressionType.Lambda)
        {
            throw new ArgumentException("Expression must be a lambda expression.", nameof(expression));
        }

        return ((MemberInfo)((ConstantExpression)((MethodCallExpression)((UnaryExpression)((LambdaExpression)expression)
                .Body).Operand).Object!).Value!).GetCustomAttributes(true)
            .OfType<LoggerMessageAttribute>()
            .First();
    }

    private void InitializeRecordedMessages()
    {
        _recordedMessages.Add(LogLevel.Critical, new List<LogMessage>());
        _recordedMessages.Add(LogLevel.Error, new List<LogMessage>());
        _recordedMessages.Add(LogLevel.Warning, new List<LogMessage>());
        _recordedMessages.Add(LogLevel.Information, new List<LogMessage>());
        _recordedMessages.Add(LogLevel.Debug, new List<LogMessage>());
        _recordedMessages.Add(LogLevel.Trace, new List<LogMessage>());
        _recordedMessages.Add(LogLevel.None, new List<LogMessage>());
    }

    private sealed class NullDisposable : IDisposable
    {
        public static readonly NullDisposable Instance = new();

        public void Dispose()
        {
        }
    }
}
