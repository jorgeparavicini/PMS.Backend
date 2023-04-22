// -----------------------------------------------------------------------
// <copyright file="EntityCustomization.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using AutoFixture;
using PMS.Backend.Test.Common.Builders;

namespace PMS.Backend.Test.Common.Customization;

public class EntityCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new EntityBuilder());
    }
}
