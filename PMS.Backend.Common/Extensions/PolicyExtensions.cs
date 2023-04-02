using System.Diagnostics;
using System.Linq;
using PMS.Backend.Common.Security;

namespace PMS.Backend.Common.Extensions;

/// <summary>
/// Extensions for the <see cref="Policy"/> enumeration.
/// </summary>
public static class PolicyExtensions
{
    /// <summary>
    /// Gets the auth0 scope for a policy.
    /// </summary>
    /// <param name="policy">The policy to convert to a scope.</param>
    /// <returns>The auth0 scope.</returns>
    public static string GetScope(this Policy policy)
    {
        var name = policy.ToString();
        var split = name.SplitCamelCase().Select(x => x.ToLower()).ToList();

        Debug.Assert(split.Count >= 2,
            "The name of policy must start with an operation followed by the name of the target.");

        return $"{split[0]}:{string.Join("-", split.Skip(1))}";
    }
}
