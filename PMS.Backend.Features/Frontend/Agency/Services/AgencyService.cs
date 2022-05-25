using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Frontend.Agency.Models;
using PMS.Backend.Features.Frontend.Agency.Models.Output;
using PMS.Backend.Features.Frontend.Agency.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Agency.Services;

public class AgencyService : IAgencyService
{
    private readonly PmsDbContext _context;
    private readonly IMapper _mapper;

    public AgencyService(PmsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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
}