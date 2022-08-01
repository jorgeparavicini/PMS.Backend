using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Frontend.Agency.Controllers;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;
using PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.Mock;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests;

public class AgencyControllerTest
{
    #region Agency

    [Fact]
    public async Task GetAll_ShouldReturn200OkObjectStatus()
    {
        // Arrange
        var agencyService = new Mock<IAgencyService>();
        agencyService
            .Setup(x => x.GetAllAgenciesAsync())
            .ReturnsAsync(AgencyMockData.GetAgencySummaries);

        var sut = new AgenciesController(agencyService.Object);

        // Act
        var result = await sut.GetAll();

        // Assert
        result.Result.Should()
            .BeOfType<OkObjectResult>()
            .Which.StatusCode.Should()
            .Be(StatusCodes.Status200OK);
    }

    [Fact]
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

    #endregion
}
