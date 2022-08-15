using System.Text;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PMS.Backend.Core.Database;
using PMS.Backend.Test.ContractResolvers;
using PMS.Backend.Test.Fixtures;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests;

public abstract class ControllerTests
{
    protected HttpClient Client { get; }
    protected IConfiguration AuthSettings { get; }
    protected PmsDbContext Context { get; }

    protected ControllerTests(PMSServerFixture fixture)
    {
        Client = fixture.Client;
        AuthSettings = fixture.Configuration;
        Context = fixture.Context;
    }

    protected async Task<string> GetAccessToken()
    {
        var auth0Client = new AuthenticationApiClient(AuthSettings["Auth0:Domain"]);
        var tokenRequest = new ClientCredentialsTokenRequest()
        {
            ClientId = AuthSettings["Auth0:ClientId"],
            ClientSecret = AuthSettings["Auth0:ClientSecret"],
            Audience = AuthSettings["Auth0:Audience"]
        };

        var tokenResponse = await auth0Client.GetTokenAsync(tokenRequest);

        return tokenResponse.AccessToken;
    }

    protected async Task<string> GetAccessTokenWithNoPermissions()
    {
        var auth0Client = new AuthenticationApiClient(AuthSettings["Auth0:Domain"]);
        var tokenRequest = new ClientCredentialsTokenRequest()
        {
            ClientId = AuthSettings["Auth0:ClientIdNoPermissions"],
            ClientSecret = AuthSettings["Auth0:ClientSecretNoPermissions"],
            Audience = AuthSettings["Auth0:Audience"]
        };

        var tokenResponse = await auth0Client.GetTokenAsync(tokenRequest);

        return tokenResponse.AccessToken;
    }

    protected StringContent SerializeObject(object? value)
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new NoReverseLookupResolver()
        };
        var content = JsonConvert.SerializeObject(value, settings);
        return new StringContent(content, Encoding.UTF8, "application/json");
    }

    protected async Task<T?> DeserializeObject<T>(HttpContent content)
    {
        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
        return JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync(), settings);
    }
}
