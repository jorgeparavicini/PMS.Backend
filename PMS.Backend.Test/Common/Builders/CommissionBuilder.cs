// -----------------------------------------------------------------------
// <copyright file="CommissionBuilder.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;

namespace PMS.Backend.Test.Common.Builders;

/// <summary>
///     A specimen builder that creates a commission value.
/// <para>
///     A commission value is a decimal in the range of 0.0000 to 9.9999.
/// </para>
/// </summary>
/// <typeparam name="TClass">The type of the class to customize.</typeparam>
public class CommissionBuilder<TClass> : ISpecimenBuilder
{
    private readonly Expression<Func<TClass, object?>>[] _properties;

    public CommissionBuilder(params Expression<Func<TClass, object?>>[] properties)
    {
        _properties = properties;
    }

    public object Create(object request, ISpecimenContext context)
    {
        if (request is not PropertyInfo propertyInfo || !typeof(TClass).IsAssignableFrom(propertyInfo.DeclaringType))
        {
            return new NoSpecimen();
        }

        Expression<Func<TClass, object?>>? matchingProperty =
            _properties.FirstOrDefault(property => GetMemberName(property) == propertyInfo.Name);

        if (matchingProperty == null)
        {
            return new NoSpecimen();
        }

        if (!IsDecimalProperty(propertyInfo))
        {
            throw new ArgumentException($"The property {propertyInfo.Name} is not of type decimal.");
        }

        return context.Create<decimal>() % 9.9m;
    }

    private static string GetMemberName<T>(Expression<Func<T, object?>> expression) => expression.Body switch
    {
        MemberExpression memberExpression => memberExpression.Member.Name,
        UnaryExpression { Operand: MemberExpression memberExpression } => memberExpression.Member.Name,
        _ => throw new ArgumentException("Expression is not a member access", nameof(expression)),
    };

    private static bool IsDecimalProperty(PropertyInfo propertyInfo)
    {
        Type propertyType = propertyInfo.PropertyType;
        return propertyType == typeof(decimal) || propertyType == typeof(decimal?);
    }
}
