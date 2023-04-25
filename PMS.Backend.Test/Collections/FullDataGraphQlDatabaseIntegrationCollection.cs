// -----------------------------------------------------------------------
// <copyright file="FullDataGraphQlDatabaseIntegrationCollection.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PMS.Backend.Test.Fixtures;
using Xunit;

namespace PMS.Backend.Test.Collections;

[CollectionDefinition(CollectionIndex.ReadonlyCollection)]
public class FullDataGraphQlDatabaseIntegrationCollection
    : ICollectionFixture<FullDataGraphQlFixture>
{
}
