using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PMS.Backend.Common.Extensions;

/// <summary>
/// Extension methods for <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Split a camel case string into their components separated by capitalized letters.
    /// </summary>
    /// <param name="source">The source string to split.</param>
    /// <returns>
    /// An <see cref="Enumerable"/> containing all components of a string separated by a
    /// capitalized letter.
    /// </returns>
    public static IEnumerable<string> SplitCamelCase(this string source)
    {
        const string pattern = @"[A-Z][a-z]*|[a-z]+|\d+";
        return Regex.Matches(source, pattern).Select(x => x.Value);
    }
}
