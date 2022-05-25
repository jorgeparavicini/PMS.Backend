using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;
using PMS.Backend.Features.Frontend.Agency.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Agency.Services;

public class AgencyService : IAgencyService
{
    private readonly PmsDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public AgencyService(PmsDbContext context, IMapper mapper, ILogger<AgencyService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<AgencyDTO>> GetAllAgenciesAsync()
    {
        var agencies = await _context.Agencies.Include(x => x.AgencyContacts).ToListAsync();
        return _mapper.Map<List<Core.Entities.Agency>, List<AgencyDTO>>(agencies);
    }

    public async Task<AgencyDTO?> FindAgencyAsync(int id)
    {
        var agency = await _context.Agencies.FindAsync(id);
        return _mapper.Map<AgencyDTO?>(agency);
    }

    public async Task<int?> CreateAgencyAsync(AgencyInputDTO agency)
    {
        var entity = _mapper.Map<Core.Entities.Agency>(agency);
        await _context.Agencies.AddAsync(entity);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            _logger.LogError("Could not save new agency to db. Error: {Message}", e.Message);
        }
        
        return entity.Id > 0 ? entity.Id : null;
    }
}