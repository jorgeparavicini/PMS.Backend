// -----------------------------------------------------------------------
// <copyright file="Environment.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace PMS.Backend.Common.Models;

/// <summary>
/// A list of all possible environments.
/// </summary>
public static class Environment
{
    /// <summary>
    /// Development environment.
    /// </summary>
    public const string Development = "Development";

    /// <summary>
    /// Staging and Testing environment.
    /// </summary>
    public const string Staging = "Staging";

    /// <summary>
    /// Production environment.
    /// </summary>
    public const string Release = "Release";
}
