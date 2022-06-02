using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Exceptions;
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
        return _mapper.Map<List<Core.Entities.Agency.Agency>, List<AgencySummaryDTO>>(agencies);
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
        var entity = _mapper.Map<Core.Entities.Agency.Agency>(agency);
        var result = entity.Validate();
        if (result.IsFailed)
        {
            throw new BadRequestException($"{result.Errors}");
        }
        
        await _context.Agencies.AddAsync(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<AgencySummaryDTO>(entity);
    }

    public async Task<AgencySummaryDTO> UpdateAgencyAsync(UpdateAgencyDTO agency)
    {
        if (await _context.Agencies.FindAsync(agency.Id) is { } entity)
        {
            _mapper.Map(agency, entity);
            var result = entity.Validate();
            if (result.IsFailed)
            {
                throw new BadRequestException($"{result.Errors}");
            }
            
            await _context.SaveChangesAsync();
            return _mapper.Map<AgencySummaryDTO>(entity);
        }

        throw new NotFoundException("Could not find agency");
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
        var agency = await _context.Agencies.FindAsync(agencyId);
        if (agency is null)
        {
            throw new NotFoundException($"Agency with id {agencyId} not found.");
        }

        return _mapper.Map<IEnumerable<AgencyContact>, IEnumerable<AgencyContactDTO>>(
            agency.AgencyContacts);
    }

    public async Task<AgencyContactDTO?> FindContactForAgency(int agencyId, int contactId)
    {
        var contact = await _context.AgencyContacts
            .FirstOrDefaultAsync(x => x.AgencyId == agencyId && x.Id == contactId);
        return _mapper.Map<AgencyContactDTO?>(contact);
    }

    public async Task<AgencyContactDTO> CreateContactForAgencyAsync(
        int agencyId,
        CreateAgencyContactDTO contact)
    {
        if (await _context.Agencies.FindAsync(agencyId) is { } agency)
        {
            var entity = _mapper.Map<AgencyContact>(contact);
            var result = entity.Validate();
            if (result.IsFailed)
            {
                throw new BadRequestException($"{result.Errors}");
            }

            agency.AgencyContacts.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AgencyContactDTO>(entity);
        }

        throw new NotFoundException("Could not find agency");
    }

    public async Task<AgencyContactDTO> UpdateContactForAgencyAsync(
        int agencyId,
        UpdateAgencyContactDTO contact)
    {
        if (await _context.AgencyContacts
                .FirstOrDefaultAsync(x => x.AgencyId == agencyId && x.Id == contact.Id)
            is { } entity)
        {
            _mapper.Map(contact, entity);
            var result = entity.Validate();
            if (result.IsFailed)
            {
                throw new BadRequestException($"{result.Errors}");
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<AgencyContactDTO>(entity);
        }

        throw new NotFoundException("Could not find agency contact");
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