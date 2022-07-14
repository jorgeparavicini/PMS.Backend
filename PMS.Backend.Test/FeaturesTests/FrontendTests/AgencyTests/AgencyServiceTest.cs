using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Common.Models;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Services;
using PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.Mock;
using Profile = PMS.Backend.Features.Frontend.Agency.Profile;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests;

public class AgencyServiceTest : IDisposable
{
    private readonly PmsDbContext _context;
    private readonly IMapper _mapper;

    public AgencyServiceTest()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<Profile>());
        _mapper = new Mapper(config);

        var options = new DbContextOptionsBuilder<PmsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new PmsDbContext(options);
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetAllAgenciesAsync_ShouldReturnAgencyCollection()
    {
        // Arrange
        _context.Agencies.AddRange(AgencyMockData.GetAgencies());
        await _context.SaveChangesAsync();

        var sut = new AgencyService(_context, _mapper);

        // Act
        var result = await sut.GetAllAgenciesAsync();

        // Assert
        result.Should().HaveCount(AgencyMockData.GetAgencies().Count);
    }

    [Fact]
    public async Task GetAllAgenciesAsync_ShouldReturnEmptyAgencyCollection()
    {
        // Arrange
        var sut = new AgencyService(_context, _mapper);

        // Act
        var result = await sut.GetAllAgenciesAsync();

        // Assert
        result.Should().HaveCount(0);
    }

    [Fact]
    public async Task FindAgencyAsync_ShouldReturnAgency()
    {
        // Arrange
        var agency = AgencyMockData.GetAgencies().First();
        _context.Agencies.AddRange(AgencyMockData.GetAgencies());
        await _context.SaveChangesAsync();

        var sut = new AgencyService(_context, _mapper);

        // Act
        var result = await sut.FindAgencyAsync(agency.Id);

        // Assert
        result.Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(AgencyMockData.GetFoundAgencySummary());
    }

    [Fact]
    public async Task FindAgencyAsync_ShouldReturnNull()
    {
        // Arrange
        _context.Agencies.AddRange(AgencyMockData.GetAgencies());
        await _context.SaveChangesAsync();

        var sut = new AgencyService(_context, _mapper);

        // Act
        var result = await sut.FindAgencyAsync(-1);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAgencyAsync_ShouldReturnBadRequest()
    {
        // Arrange
        _context.Agencies.AddRange(AgencyMockData.GetAgencies());
        await _context.SaveChangesAsync();

        var newAgency = new CreateAgencyDTO(
            null!,
            null,
            null,
            CommissionMethod.DeductedByAgency,
            null,
            null,
            new List<CreateAgencyContactDTO>());

        var sut = new AgencyService(_context, _mapper);

        // Act
        var act = async () => await sut.CreateAgencyAsync(newAgency);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task CreateAgencyAsync_ShouldCreateNewAgency()
    {
        // Arrange
        _context.Agencies.AddRange(AgencyMockData.GetAgencies());
        await _context.SaveChangesAsync();

        var newAgency = new CreateAgencyDTO(
            "Agency",
            null,
            null,
            CommissionMethod.DeductedByAgency,
            null,
            null,
            new List<CreateAgencyContactDTO>());

        var sut = new AgencyService(_context, _mapper);

        // Act
        await sut.CreateAgencyAsync(newAgency);

        // Assert
        var expectedRecordCount = AgencyMockData.GetAgencies().Count + 1;
        _context.Agencies.Count().Should().Be(expectedRecordCount);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}