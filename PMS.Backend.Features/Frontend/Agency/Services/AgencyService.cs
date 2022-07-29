using AutoMapper;
using Detached.Mappers.EntityFramework;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;
using PMS.Backend.Features.Frontend.Agency.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Agency.Services;

/// <inheritdoc/>
public class AgencyService : IAgencyService
{
    private readonly PmsDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="AgencyService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">
    /// The automapper mapper already loaded with the required profiles.
    /// </param>
    public AgencyService(PmsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<AgencySummaryDTO>> GetAllAgenciesAsync()
    {
        var agencies = await _context.Agencies.ToListAsync();

        return _mapper.Map<IEnumerable<Core.Entities.Agency.Agency>, IEnumerable<AgencySummaryDTO>>(
            agencies);
    }

    /// <inheritdoc/>
    public async Task<AgencyDetailDTO?> FindAgencyAsync(int id)
    {
        var agency = await _context.Agencies
            .Include(x => x.AgencyContacts)
            .FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<AgencyDetailDTO?>(agency);
    }

    /// <inheritdoc/>
    public async Task<AgencySummaryDTO> CreateAgencyAsync(CreateAgencyDTO input)
    {
        var entity = await _context.MapAsync<Core.Entities.Agency.Agency>(input);
        entity.ValidateIds(_context);
        await _context.SaveChangesAsync();

        return _mapper.Map<AgencySummaryDTO>(entity);
    }

    /// <inheritdoc/>
    public async Task<AgencySummaryDTO> UpdateAgencyAsync(UpdateAgencyDTO input)
    {
        var entity = await _context.MapAsync<Core.Entities.Agency.Agency>(input);
        entity.ValidateIds(_context);
        await _context.SaveChangesAsync();

        return _mapper.Map<AgencySummaryDTO>(entity);
    }

    /// <inheritdoc/>
    /// TODO: Prevent deletion if contacts are referenced.
    public async Task DeleteAgencyAsync(int id)
    {
        if (await _context.Agencies.FindAsync(id) is { } agency)
        {
            _context.Agencies.Remove(agency);
            await _context.SaveChangesAsync();
        }
    }
}
