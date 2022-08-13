using System.Net;
using System.Net.Http.Headers;
using Detached.Mappers.EntityFramework;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Test.Collections;
using PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.Mock;
using PMS.Backend.Test.Fixtures;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests;

[Collection(PMSCollection.Name)]
public class AgencyControllerTest : ControllerTests
{
    public AgencyControllerTest(
        PMSServerFixture fixture)
        : base(fixture)
    {
    }

    [Theory]
    [MemberData(nameof(AgencyMockData.Endpoints), MemberType = typeof(AgencyMockData))]
    public async Task EndpointsWithoutAuthorization_ShouldReturn401Unauthorized(
        string endpoint,
        HttpMethod method)
    {
        // Arrange
        var request = new HttpRequestMessage(method, endpoint);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Theory]
    [MemberData(nameof(AgencyMockData.Endpoints), MemberType = typeof(AgencyMockData))]
    public async Task EndpointsWithInvalidBearerToken_ShouldReturn401Unauthorized(
        string endpoint,
        HttpMethod method)
    {
        // Arrange
        var request = new HttpRequestMessage(method, endpoint);

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", "This is an invalid token");

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Theory]
    [MemberData(nameof(AgencyMockData.Endpoints), MemberType = typeof(AgencyMockData))]
    public async Task EndpointsWithNoPermissionsBearerToken_ShouldReturn403Forbidden(
        string endpoint,
        HttpMethod method)
    {
        // Arrange
        var request = new HttpRequestMessage(method, endpoint);

        var accessToken = await GetAccessTokenWithNoPermissions();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetAll_ShouldReturn200Ok()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "agencies");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Find_ShouldReturn200OkAndContainSingleEntity()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "agencies(1)");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var agencies =
            JsonConvert.DeserializeObject<Agency>(await response.Content.ReadAsStringAsync());
        agencies.Should().NotBeNull();
    }

    [Fact]
    public async Task Find_ShouldReturn404NotFound()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "agencies(-1)");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Create_ShouldReturn201Created()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Post, "agencies");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        request.Content = SerializeObject(AgencyMockData.GetCreateMockAgency());

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Create_ShouldReturn204NoContent()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Post, "agencies");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("Prefer", "return=minimal");

        request.Content = SerializeObject(AgencyMockData.GetCreateMockAgency());

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Create_ShouldReturn400BadRequest()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Post, "agencies");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        request.Content = SerializeObject(null);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_ShouldReturn200Ok()
    {
        // Arrange
        var entity = await Context.MapAsync<Agency>(AgencyMockData.GetCreateMockAgency());
        await Context.SaveChangesAsync();

        var request = new HttpRequestMessage(HttpMethod.Put, $"agencies({entity.Id})");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("Prefer", "return=representation");

        request.Content = SerializeObject(entity);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Update_ShouldReturn204NoContent()
    {
        // Arrange
        var entity = await Context.MapAsync<Agency>(AgencyMockData.GetCreateMockAgency());
        await Context.SaveChangesAsync();

        var request = new HttpRequestMessage(HttpMethod.Put, $"agencies({entity.Id})");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        request.Content = SerializeObject(entity);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Update_ShouldReturn400BadRequest()
    {
        // Arrange
        var entity = await Context.MapAsync<Agency>(AgencyMockData.GetCreateMockAgency());
        await Context.SaveChangesAsync();

        var request = new HttpRequestMessage(HttpMethod.Put, $"agencies({entity.Id})");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        request.Content = SerializeObject(null);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Update_ShouldReturn404NotFound()
    {
        // Arrange
        var entity = await Context.MapAsync<Agency>(AgencyMockData.GetCreateMockAgency());
        await Context.SaveChangesAsync();

        // Ef core does not allow changing IDs of tracked entities.
        // As we just added the entity we must disable tracking for this entity.
        Context.ChangeTracker.Entries().First(x => x.Entity == entity).State = EntityState.Detached;
        entity.Id = int.MaxValue;

        var request = new HttpRequestMessage(HttpMethod.Put, $"agencies({entity.Id})");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        request.Content = SerializeObject(entity);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task DeleteWithInvalidId_ShouldReturn204NoContent()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Delete, $"agencies(-1)");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteWithValidId_ShouldReturn204NoContent()
    {
        // Arrange
        var entity = await Context.MapAsync<Agency>(AgencyMockData.GetCreateMockAgency());
        await Context.SaveChangesAsync();

        var request = new HttpRequestMessage(HttpMethod.Delete, $"agencies({entity.Id})");

        var accessToken = await GetAccessToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Act
        var response = await Client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
