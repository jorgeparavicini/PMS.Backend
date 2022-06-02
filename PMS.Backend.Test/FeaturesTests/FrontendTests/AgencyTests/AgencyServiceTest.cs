using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Database;
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
    public async Task GetAllAgenciesAsync_ReturnAgencyCollection()
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
    
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}