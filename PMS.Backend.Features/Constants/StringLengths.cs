namespace PMS.Backend.Features.Constants;

/// <summary>
/// Provides constants that define string lengths used across the application.
/// </summary>
public static class StringLengths
{
    /// <summary>
    /// Typical length for codes, tags, or abbreviations.
    /// </summary>
    public const int Code = 10;

    /// <summary>
    /// Standard length for short names or titles.
    /// </summary>
    public const int ShortName = 50;

    /// <summary>
    /// Extended length for full names which might include middle names, prefixes, and suffixes.
    /// </summary>
    public const int FullName = 150;

    /// <summary>
    /// Standard length for email addresses.
    /// </summary>
    public const int Email = 255;

    /// <summary>
    /// Length for storing URLs or URIs.
    /// </summary>
    public const int Url = 2000;

    /// <summary>
    /// Standard length for textual descriptions.
    /// </summary>
    public const int Description = 500;

    /// <summary>
    /// Extended length for longer textual content, comments, or feedback.
    /// </summary>
    public const int LongDescription = 2000;

    /// <summary>
    /// Typical length for password hashes (e.g., BCrypt).
    /// </summary>
    public const int PasswordHash = 60;

    /// <summary>
    /// Typical length for phone numbers considering international formats.
    /// </summary>
    public const int PhoneNumber = 20;

    /// <summary>
    /// Length for address fields like street names.
    /// </summary>
    public const int Address = 255;

    /// <summary>
    /// Standard length for city names.
    /// </summary>
    public const int City = 100;

    /// <summary>
    /// Standard length for state, province, or region names.
    /// </summary>
    public const int State = 100;

    /// <summary>
    /// Standard length for country names.
    /// </summary>
    public const int Country = 100;

    /// <summary>
    /// Typical length for postal or zip codes.
    /// </summary>
    public const int PostalCode = 20;

    /// <summary>
    /// Length for general textual fields without specific length constraints.
    /// </summary>
    public const int GeneralText = 255;
}
