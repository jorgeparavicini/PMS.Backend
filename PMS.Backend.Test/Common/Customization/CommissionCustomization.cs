// -----------------------------------------------------------------------
// <copyright file="CommissionCustomization.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using AutoFixture;
using PMS.Backend.Test.Common.Builders;

namespace PMS.Backend.Test.Common.Customization;

/// <summary>
///     A customization that converts decimals to a commission value.
/// <para>
///     This is used to ensure that the commission value is in the range of 0.0000 to 9.9999.
/// </para>
/// </summary>
/// <typeparam name="TClass">The type of the class to customize.</typeparam>
public class CommissionCustomization<TClass> : ICustomization
{
    private readonly Expression<Func<TClass, object?>>[] _properties;

    public CommissionCustomization(params Expression<Func<TClass, object?>>[] properties)
    {
        _properties = properties;
    }

    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new CommissionBuilder<TClass>(_properties));
    }
}
