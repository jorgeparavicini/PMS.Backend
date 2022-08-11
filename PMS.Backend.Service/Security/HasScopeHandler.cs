﻿using Microsoft.AspNetCore.Authorization;

namespace PMS.Backend.Service.Security;

/// <summary>
/// An <see cref="AuthorizationHandler{T}"/> handling auth0 scope requirements.
/// </summary>
public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    /// <inheritdoc />
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        HasScopeRequirement requirement)
    {
        // First check for permissions, they may show up in addition to or instead of scopes.
        if (context.User.HasClaim(c =>
                c.Type == "permissions" && c.Issuer == requirement.Issuer &&
                c.Value == requirement.Scope))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        // If user does not have the scope claim, get out of here
        if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
            return Task.CompletedTask;

        // Split the scopes string into an array
        var scopes =
            context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer)!.Value
                .Split(' ');

        // Succeed if the scope array contains the required scope
        if (scopes.Any(s => s == requirement.Scope))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
