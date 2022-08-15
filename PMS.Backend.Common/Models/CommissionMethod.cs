namespace PMS.Backend.Common.Models;

/// <summary>
/// A commission method dictates how sales should be commissioned.
/// <para>Default value: <code>DeductedByAgency</code></para>
/// </summary>
public enum CommissionMethod
{
    /// <summary>
    /// The sale should be commissioned by the agency.
    /// </summary>
    /// <value>0</value>
    DeductedByAgency = 0,

    /// <summary>
    /// The sale should be commissioned by the provider directly.
    /// </summary>
    /// <remarks>
    /// The provider is usually the hotel or establishment that provides the service.
    /// </remarks>
    /// <value>1</value>
    DeductedByProvider = 1,
}
