using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace PMS.Backend.Service.Extensions;

/// <summary>
///     Extension methods for <see cref="ConfigurationManager" />.
/// </summary>
public static class ConfigurationManagerExtensions
{
    /// <summary>
    ///     Adds the environment specific configuration file to the <see cref="ConfigurationManager" />.
    /// </summary>
    /// <param name="configurationManager">
    ///     The <see cref="ConfigurationManager" /> to add the configuration file to.
    /// </param>
    /// <param name="environment">
    ///     The <see cref="IHostEnvironment" /> instance that holds the environment data.
    /// </param>
    /// <returns>
    ///     The <see cref="ConfigurationManager" /> so that additional calls can be chained.
    /// </returns>
    public static ConfigurationManager AddEnvironmentConfiguration(
        this ConfigurationManager configurationManager,
        IHostEnvironment environment)
    {
        string env = environment.EnvironmentName;
        configurationManager.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);
        return configurationManager;
    }
}
