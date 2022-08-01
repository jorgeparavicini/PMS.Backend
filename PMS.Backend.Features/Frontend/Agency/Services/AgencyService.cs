using AutoMapper;
using Detached.Mappers.EntityFramework;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Input.Validation;
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
    public IQueryable<Core.Entities.Agency.Agency> GetAllAgencies()
    {
        return _context.Agencies;
    }

    /// <inheritdoc/>
    public IQueryable<Core.Entities.Agency.Agency> FindAgencyAsync(int id)
    {
        return _context.Agencies.Where(x => x.Id == id);
    }

    /// <inheritdoc/>
    public async Task<Core.Entities.Agency.Agency> CreateAgencyAsync(CreateAgencyDTO input)
    {
        var entity = await _context
            .ValidateAndMapAsync<
                Core.Entities.Agency.Agency,
                CreateAgencyDTO,
                CreateAgencyDTOValidator>(input);

        await _context.SaveChangesAsync();

        return entity;
    }

    /// <inheritdoc/>
    public async Task<Core.Entities.Agency.Agency> UpdateAgencyAsync(int id, UpdateAgencyDTO input)
    {
        if (id != input.Id)
        {
            throw new BadRequestException(
                $"The query id {id} does not match the DTO id {input.Id}");
        }
        var entity = await _context.MapAsync<Core.Entities.Agency.Agency>(input);
        entity.ValidateIds(_context);
        await _context.SaveChangesAsync();

        return entity;
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
