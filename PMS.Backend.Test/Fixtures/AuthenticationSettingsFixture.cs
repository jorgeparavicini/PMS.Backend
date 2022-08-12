using Microsoft.Extensions.Configuration;

namespace PMS.Backend.Test.Fixtures;

public class AuthenticationSettingsFixture
{
    public IConfiguration Config { get; }

    public AuthenticationSettingsFixture()
    {
        Config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Integration.json")
            .Build()
            .GetSection("auth0");
    }
}
