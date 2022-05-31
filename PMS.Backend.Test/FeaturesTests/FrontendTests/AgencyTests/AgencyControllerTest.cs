using System.Net;
using System.Text.Unicode;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Frontend.Agency.Controllers;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;
using PMS.Backend.Features.Frontend.Agency.Services;
using PMS.Backend.Features.Frontend.Agency.Services.Contracts;
using PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.Mock;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests;

public class AgencyControllerTest
{
    [Fact]
    public async Task GetAll_ShouldReturn200OkObjectStatus()
    {
        // Arrange
        var agencyService = new Mock<IAgencyService>();
        agencyService.Setup(x => x.GetAllAgenciesAsync())
            .ReturnsAsync(AgencyMockData.GetAgencySummaries);
        var sut = new AgencyController(agencyService.Object);

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
        agencyService.Setup(x => x.GetAllAgenciesAsync())
            .ReturnsAsync(AgencyMockData.GetEmptyAgencies());
        var sut = new AgencyController(agencyService.Object);

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
        agencyService.Setup(x => x.FindAgencyAsync(It.IsAny<int>()))
            .ReturnsAsync(AgencyMockData.GetAgencyDetail());
        var sut = new AgencyController(agencyService.Object);

        // Act
        var result = await sut.Find(1);

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
        agencyService.Setup(x => x.FindAgencyAsync(It.IsAny<int>()))
            .ReturnsAsync(null as AgencyDetailDTO);
        var sut = new AgencyController(agencyService.Object);

        // Act
        var result = await sut.Find(1);

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
            .ReturnsAsync(AgencyMockData.CreatedAgencySummary);
        var sut = new AgencyController(agencyService.Object);

        // Act
        var result = await sut.Create(AgencyMockData.CreateAgencyDTO());

        // Assert
        result.Result.Should()
            .BeOfType<CreatedAtActionResult>()
            .Which.StatusCode.Should()
            .Be(StatusCodes.Status201Created);

        result.Result.Should()
            .BeOfType<CreatedAtActionResult>()
            .Which.RouteValues.Should()
            .Contain(nameof(AgencySummaryDTO.Id), AgencyMockData.CreatedAgencySummary().Id);

        result.Result.Should()
            .BeOfType<CreatedAtActionResult>()
            .Which.ActionName.Should()
            .BeEquivalentTo(nameof(AgencyController.Find));
    }

    [Fact]
    public async Task Create_ShouldReturn404BadRequest()
    {
        // Arrange
        var agencyService = new Mock<IAgencyService>();
        agencyService.Setup(x => x.CreateAgencyAsync(It.IsAny<CreateAgencyDTO>()))
            .Throws<BadRequestException>();
        var sut = new AgencyController(agencyService.Object);

        // Act
        var result = await sut.Create(AgencyMockData.CreateAgencyDTO());

        // Assert
        result.Result.Should()
            .BeOfType<BadRequestObjectResult>()
            .Which.StatusCode.Should()
            .Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Update_ShouldReturn400BadRequest()
    {
        // Arrange
        var agencyService = new Mock<IAgencyService>();
        agencyService.Setup(x => x.UpdateAgencyAsync(It.IsAny<UpdateAgencyDTO>()))
            .Throws<BadRequestException>();
        var sut = new AgencyController(agencyService.Object);

        // Act
        var result = await sut.Update(AgencyMockData.UpdateAgencyDTO().Id - 1,
            AgencyMockData.UpdateAgencyDTO());
        
        // Assert
        result.Result.Should()
            .BeOfType<BadRequestObjectResult>()
            .Which.StatusCode.Should()
            .Be(StatusCodes.Status400BadRequest);
        result.Result.Should()
            .BeOfType<BadRequestObjectResult>()
            .Which.Value.Should()
            .BeEquivalentTo("Agency Id mismatch");
        
        // Act
        result = await sut.Update(AgencyMockData.UpdateAgencyDTO().Id,
            AgencyMockData.UpdateAgencyDTO());
        
        // Assert
        result.Result.Should()
            .BeOfType<BadRequestObjectResult>()
            .Which.StatusCode.Should()
            .Be(StatusCodes.Status400BadRequest);
        
        result.Result.Should()
            .BeOfType<BadRequestObjectResult>()
            .Which.Value.Should()
            .BeOfType<string>()
            .Which.Should()
            .BeNullOrEmpty();
    }

    [Fact]
    public async Task Update_ShouldReturn404NotFound()
    {
        // Arrange
        var agencyService = new Mock<IAgencyService>();
        agencyService.Setup(x => x.UpdateAgencyAsync(It.IsAny<UpdateAgencyDTO>()))
            .Throws<NotFoundException>();
        var sut = new AgencyController(agencyService.Object);
        
        // Act
        var result = await sut.Update(AgencyMockData.UpdateAgencyDTO().Id,
            AgencyMockData.UpdateAgencyDTO());
        
        // Assert
        result.Result.Should()
            .BeOfType<NotFoundObjectResult>()
            .Which.StatusCode.Should()
            .Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Update_ShouldReturn200Ok()
    {
        // Arrange
        var agencyService = new Mock<IAgencyService>();
        agencyService.Setup(x => x.UpdateAgencyAsync(It.IsAny<UpdateAgencyDTO>()))
            .ReturnsAsync(AgencyMockData.UpdatedAgencySummary);
        var sut = new AgencyController(agencyService.Object);
        
        // Act
        var result = await sut.Update(AgencyMockData.UpdateAgencyDTO().Id,
            AgencyMockData.UpdateAgencyDTO());
        
        // Assert
        result.Result.Should()
            .BeOfType<OkObjectResult>()
            .Which.StatusCode.Should()
            .Be(StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Delete_ShouldReturn204NoContent()
    {
        // Arrange
        var agencyService = new Mock<IAgencyService>();
        var sut = new AgencyController(agencyService.Object);
        
        // Act
        var result = await sut.Delete(0);
        
        // Assert
        result.Should()
            .BeOfType<NoContentResult>()
            .Which.StatusCode.Should()
            .Be(StatusCodes.Status204NoContent);
    }
}