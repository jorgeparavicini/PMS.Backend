using Microsoft.AspNetCore.Authorization;

namespace PMS.Backend.Service.Security;

/// <summary>
/// An authorization requirement that validates an Auth0 scope.
/// </summary>
public class HasScopeRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// The issuer of the scope.
    /// </summary>
    public string Issuer { get; init; }

    /// <summary>
    /// The name of the scope.
    /// </summary>
    /// <remarks>
    /// This name must be equal to the one provided in auth0.
    /// </remarks>
    public string Scope { get; init; }

    /// <summary>
    /// Initializes a new <see cref="HasScopeRequirement"/> instance.
    /// </summary>
    /// <param name="scope">The name of the scope.</param>
    /// <param name="issuer">The issuer of the scope.</param>
    public HasScopeRequirement(string scope, string issuer)
    {
        Issuer = issuer;
        Scope = scope;
    }
}
