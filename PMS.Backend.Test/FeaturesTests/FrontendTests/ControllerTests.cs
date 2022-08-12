using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.Extensions.Configuration;
using PMS.Backend.Test.Fixtures;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests;

public abstract class ControllerTests
{
    protected HttpClient Client { get; }
    protected IConfiguration AuthSettings { get; }

    protected ControllerTests(PMSServerFixture fixture, AuthenticationSettingsFixture authFixture)
    {
        Client = fixture.Client;
        AuthSettings = authFixture.Config;
    }

    protected async Task<string> GetAccessToken()
    {
        var auth0Client = new AuthenticationApiClient(AuthSettings["Domain"]);
        var tokenRequest = new ClientCredentialsTokenRequest()
        {
            ClientId = AuthSettings["ClientId"],
            ClientSecret = AuthSettings["ClientSecret"],
            Audience = AuthSettings["Audience"]
        };

        var tokenResponse = await auth0Client.GetTokenAsync(tokenRequest);

        return tokenResponse.AccessToken;
    }

    protected async Task<string> GetAccessTokenWithNoPermissions()
    {
        var auth0Client = new AuthenticationApiClient(AuthSettings["Domain"]);
        var tokenRequest = new ClientCredentialsTokenRequest()
        {
            ClientId = AuthSettings["ClientIdNoPermissions"],
            ClientSecret = AuthSettings["ClientSecretNoPermissions"],
            Audience = AuthSettings["Audience"]
        };

        var tokenResponse = await auth0Client.GetTokenAsync(tokenRequest);

        return tokenResponse.AccessToken;
    }
}
