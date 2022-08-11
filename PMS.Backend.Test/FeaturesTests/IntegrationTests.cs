using System.Net;
using System.Net.Http.Headers;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace PMS.Backend.Test.FeaturesTests;

public class IntegrationTests  : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly IConfigurationSection _auth0Settings;

    public IntegrationTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();

        _auth0Settings = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Integration.json")
            .Build()
            .GetSection("auth0");
    }

    [Theory]
    [InlineData("agencies")]
    [InlineData("reservations")]
    public async Task GetEndpointsWithoutAuthorization_ShouldReturn401Unauthorized(string endpoint)
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Theory]
    [InlineData("agencies")]
    public async Task GetEndpointsWithInvalidBearerToken_ShouldReturn401Unauthorized(string endpoint)
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Post, endpoint);

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", "This is an invalid token");

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    private async Task<string> GetAccessToken()
    {
        var auth0Client = new AuthenticationApiClient(_auth0Settings["Domain"]);
        var tokenRequest = new ClientCredentialsTokenRequest()
        {
            ClientId = _auth0Settings["ClientId"],
            ClientSecret = _auth0Settings["ClientSecret"],
            Audience = _auth0Settings["Audience"]
        };

        var tokenResponse = await auth0Client.GetTokenAsync(tokenRequest);

        return tokenResponse.AccessToken;
    }

    [Fact]
    public async Task AddTermWithAuthorization()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "agencies");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Act
        var response = await _httpClient.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
