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

    /*[Fact]
    public void GetAll_ShouldReturn200OkObjectStatus()
    {
        // Arrange
        var agencyService = new Mock<Service<Agency>>();
        //agencyService
        //    .Setup(x => x.GetAll())
        //    .Returns(AgencyMockData.GetAgencies());

        var sut = new AgenciesController(agencyService.Object);

        // Act
        var result = sut.GetAll();

        // Assert
        result.Should()
            .BeOfType<OkObjectResult>()
            .Which.StatusCode.Should()
            .Be(StatusCodes.Status200OK);
    }*/

    /* [Fact]
     public async Task GetAll_ShouldReturn204NoContentStatus()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x => x.GetAllAgenciesAsync())
             .ReturnsAsync(AgencyMockData.GetEmptyAgencies());

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.GetAll();

         // Assert
         result.Result.Should()
             .BeOfType<NoContentResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status204NoContent);
     }

     [Fact]
     public async Task Find_ShouldReturn200OkObjectStatus()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x => x.FindAgencyAsync(It.IsAny<int>()))
             .ReturnsAsync(AgencyMockData.GetAgencyDetail());

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.Find(0);

         // Assert
         result.Result.Should()
             .BeOfType<OkObjectResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status200OK);
     }

     [Fact]
     public async Task Find_ShouldReturn404NotFoundStatus()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x => x.FindAgencyAsync(It.IsAny<int>()))
             .ReturnsAsync(null as AgencyDetailDTO);

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.Find(0);

         // Assert
         result.Result.Should()
             .BeOfType<NotFoundResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status404NotFound);
     }

     [Fact]
     public async Task Create_ShouldReturn201Created()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x => x.CreateAgencyAsync(It.IsAny<CreateAgencyDTO>()))
             .ReturnsAsync(AgencyMockData.GetCreatedAgencySummary);

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.Create(AgencyMockData.GetCreateAgencyDTO());

         // Assert
         var constraint = result.Result.Should()
             .BeOfType<CreatedAtActionResult>();

         // Assert status code
         constraint.Which.StatusCode.Should()
             .Be(StatusCodes.Status201Created);

         // Assert route values
         constraint.Which.RouteValues.Should()
             .Contain(nameof(AgencySummaryDTO.Id), AgencyMockData.GetCreatedAgencySummary().Id);

         // Assert route name
         constraint.Which.ActionName.Should()
             .BeEquivalentTo(nameof(AgenciesController.Find));
     }

     [Fact]
     public async Task Update_ShouldReturn200Ok()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x => x.UpdateAgencyAsync(It.IsAny<UpdateAgencyDTO>()))
             .ReturnsAsync(AgencyMockData.GetUpdatedAgencySummary);

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.Update(
             AgencyMockData.GetUpdateAgencyDTO().Id,
             AgencyMockData.GetUpdateAgencyDTO());

         // Assert
         result.Result.Should()
             .BeOfType<OkObjectResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status200OK);
     }

     [Fact]
     public async Task Update_ShouldReturn404NotFound()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x => x.UpdateAgencyAsync(It.IsAny<UpdateAgencyDTO>()))
             .Throws<NotFoundException>();

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.Update(
             AgencyMockData.GetUpdateAgencyDTO().Id,
             AgencyMockData.GetUpdateAgencyDTO());

         // Assert
         result.Result.Should()
             .BeOfType<NotFoundObjectResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status404NotFound);
     }

     [Fact]
     public async Task Delete_ShouldReturn204NoContent()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.Delete(0);

         // Assert
         result.Should()
             .BeOfType<NoContentResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status204NoContent);
     }

     #endregion

     #region Agency Contact

     [Fact]
     public async Task FindAllContactsForAgency_ShouldReturn200Ok()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x => x.GetAllContactsForAgencyAsync(It.IsAny<int>()))
             .ReturnsAsync(AgencyMockData.GetAgencyContacts);

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.FindAllContactsForAgency(0);

         // Assert
         result.Result.Should()
             .BeOfType<OkObjectResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status200OK);
     }

     [Fact]
     public async Task FindAllContactsForAgency_ShouldReturn204NoContent()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x => x.GetAllContactsForAgencyAsync(It.IsAny<int>()))
             .ReturnsAsync(AgencyMockData.GetEmptyAgencyContacts);

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.FindAllContactsForAgency(0);

         // Assert
         result.Result.Should()
             .BeOfType<NoContentResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status204NoContent);
     }

     [Fact]
     public async Task FindAllContactsForAgency_ShouldReturn404NotFound()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x => x.GetAllContactsForAgencyAsync(It.IsAny<int>()))
             .Throws<NotFoundException>();

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.FindAllContactsForAgency(0);

         // Assert
         result.Result.Should()
             .BeOfType<NotFoundObjectResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status404NotFound);
     }

     [Fact]
     public async Task FindContact_ShouldReturn200Ok()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x => x.FindContactForAgency(
                 It.IsAny<int>(),
                 It.IsAny<int>()))
             .ReturnsAsync(AgencyMockData.GetAgencyContact);

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.FindContact(0, 0);

         // Assert
         result.Result.Should()
             .BeOfType<OkObjectResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status200OK);
     }

     [Fact]
     public async Task FindContact_ShouldReturn404NotFound()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x => x.FindContactForAgency(
                 It.IsAny<int>(),
                 It.IsAny<int>()))
             .ReturnsAsync(null as AgencyContactDTO);

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.FindContact(0, 0);

         // Assert
         result.Result.Should()
             .BeOfType<NotFoundResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status404NotFound);
     }

     [Fact]
     public async Task CreateContact_ShouldReturn201Created()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x =>
                 x.CreateContactForAgencyAsync(
                     It.IsAny<int>(),
                     It.IsAny<CreateAgencyContactDTO>()))
             .ReturnsAsync(AgencyMockData.GetAgencyContact);

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.CreateContact(0, AgencyMockData.GetCreateAgencyContactDTO());

         // Assert
         var constraint = result.Result.Should()
             .BeOfType<CreatedAtActionResult>();

         // Assert status code
         constraint.Which.StatusCode.Should()
             .Be(StatusCodes.Status201Created);

         // Assert route values
         constraint.Which.RouteValues.Should()
             .HaveCount(2)
             .And
             .Contain(nameof(AgencyContactDTO.Id), AgencyMockData.GetAgencyContact().Id);

         // Assert route name
         constraint.Which.ActionName.Should()
             .BeEquivalentTo(nameof(AgenciesController.FindContact));
     }

     [Fact]
     public async Task CreateContact_ShouldReturn404NotFound()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x =>
                 x.CreateContactForAgencyAsync(
                     It.IsAny<int>(),
                     It.IsAny<CreateAgencyContactDTO>()))
             .Throws<NotFoundException>();

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.CreateContact(0, AgencyMockData.GetCreateAgencyContactDTO());

         // Assert
         result.Result.Should()
             .BeOfType<NotFoundObjectResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status404NotFound);
     }

     [Fact]
     public async Task UpdateContact_ShouldReturn200Ok()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x =>
                 x.UpdateContactForAgencyAsync(
                     It.IsAny<int>(),
                     It.IsAny<UpdateAgencyContactDTO>()))
             .ReturnsAsync(AgencyMockData.GetAgencyContact);

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.UpdateContact(0,
             AgencyMockData.GetUpdateAgencyContactDTO().Id,
             AgencyMockData.GetUpdateAgencyContactDTO());

         // Assert
         result.Result.Should()
             .BeOfType<OkObjectResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status200OK);
     }

     [Fact]
     public async Task UpdateContact_ShouldReturn404NotFound()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();
         agencyService
             .Setup(x =>
                 x.UpdateContactForAgencyAsync(
                     It.IsAny<int>(),
                     It.IsAny<UpdateAgencyContactDTO>()))
             .Throws<NotFoundException>();

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.UpdateContact(0,
             AgencyMockData.GetUpdateAgencyContactDTO().Id,
             AgencyMockData.GetUpdateAgencyContactDTO());

         // Assert
         result.Result.Should()
             .BeOfType<NotFoundObjectResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status404NotFound);
     }

     [Fact]
     public async Task DeleteContact_ShouldReturn204NoContent()
     {
         // Arrange
         var agencyService = new Mock<IAgencyService>();

         var sut = new AgenciesController(agencyService.Object);

         // Act
         var result = await sut.DeleteAgencyContact(0, 0);

         // Assert
         result.Should()
             .BeOfType<NoContentResult>()
             .Which.StatusCode.Should()
             .Be(StatusCodes.Status204NoContent);
     }

     #endregion*/
}
