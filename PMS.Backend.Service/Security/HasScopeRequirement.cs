// -----------------------------------------------------------------------
// <copyright file="HasScopeRequirement.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;

namespace PMS.Backend.Service.Security;

/// <summary>
/// An authorization requirement that validates an Auth0 scope.
/// </summary>
public class HasScopeRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Gets the issuer of the scope.
    /// </summary>
    public required string Issuer { get; init; }

    /// <summary>
    /// Gets the name of the scope.
    /// </summary>
    /// <remarks>
    /// This name must be equal to the one provided in auth0.
    /// </remarks>
    public required string Scope { get; init; }
}
