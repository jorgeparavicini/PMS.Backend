using Microsoft.AspNetCore.Authorization;

namespace PMS.Backend.Service.Security;

public class HasScopeRequirement : IAuthorizationRequirement
{
    public string Issuer { get; init; }
    public string Scope { get; init; }

    public HasScopeRequirement(string issuer, string scope)
    {
        Issuer = issuer;
        Scope = scope;
    }
}
