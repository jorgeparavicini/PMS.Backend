using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
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

    public async Task<IEnumerable<AgencySummaryDTO>> GetAllAgenciesAsync()
    {
        var agencies = await _context.Agencies.ToListAsync();
        return _mapper.Map<List<Core.Entities.Agency>, List<AgencySummaryDTO>>(agencies);
    }

    public async Task<AgencyDetailDTO?> FindAgencyAsync(int id)
    {
        var agency = await _context.Agencies
            .Include(x => x.AgencyContacts)
            .FirstOrDefaultAsync(x => x.Id == id);
        return _mapper.Map<AgencyDetailDTO?>(agency);
    }

    public async Task<AgencySummaryDTO> CreateAgencyAsync(CreateAgencyDTO agency)
    {
        var entity = _mapper.Map<Core.Entities.Agency>(agency);
        await _context.Agencies.AddAsync(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<AgencySummaryDTO>(entity);
    }

    public async Task<AgencySummaryDTO?> UpdateAgencyAsync(UpdateAgencyDTO agency)
    {
        if (await _context.Agencies.FindAsync(agency.Id) is { } entity)
        {
            _mapper.Map(agency, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AgencySummaryDTO>(entity);
        }

        return null;
    }

    public async Task DeleteAgencyAsync(int id)
    {
        if (await _context.Agencies.FindAsync(id) is { } agency)
        {
            _context.Agencies.Remove(agency);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<AgencyContactDTO>> GetAllContactsForAgencyAsync(int agencyId)
    {
        var contacts = await _context.AgencyContacts
            .Where(x => x.AgencyId == agencyId)
            .ToListAsync();
        return _mapper.Map<List<AgencyContact>, List<AgencyContactDTO>>(contacts);
    }

    public async Task<AgencyContactDTO?> FindContactForAgency(int agencyId, int contactId)
    {
        var contact = await _context.AgencyContacts
            .FirstOrDefaultAsync(x => x.AgencyId == agencyId && x.Id == contactId);
        return _mapper.Map<AgencyContactDTO?>(contact);
    }

    // TODO: Use https://github.com/altmann/FluentResults
    public async Task<AgencyContactDTO?> CreateContactForAgencyAsync(
        int agencyId,
        CreateAgencyContactDTO contact)
    {
        if (await _context.Agencies.FindAsync(agencyId) is { } agency)
        {
            var entity = _mapper.Map<AgencyContact>(contact);
            agency.AgencyContacts.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AgencyContactDTO>(entity);
        }

        return null;
    }

    public async Task<AgencyContactDTO?> UpdateContactForAgencyAsync(
        int agencyId,
        UpdateAgencyContactDTO contact)
    {
        if (await _context.AgencyContacts
                .FirstOrDefaultAsync(x => x.AgencyId == agencyId && x.Id == contact.Id)
            is { } entity)
        {
            _mapper.Map(contact, entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AgencyContactDTO>(entity);
        }

        return null;
    }

    public async Task DeleteAgencyContactAsync(int agencyId, int contactId)
    {
        if (await _context.AgencyContacts.FirstOrDefaultAsync(x =>
                x.AgencyId == agencyId && x.Id == contactId)
            is { } entity)
        {
            _context.AgencyContacts.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}