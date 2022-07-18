using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Frontend.Reservation.Models.Input;
using PMS.Backend.Features.Frontend.Reservation.Models.Output;
using PMS.Backend.Features.Frontend.Reservation.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Reservation.Services;

/// <inheritdoc />
public class ReservationService : IReservationService
{
    private readonly PmsDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of teh <see cref="ReservationService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="mapper">
    /// The automapper mapper already loaded with the required profiles.
    /// </param>
    public ReservationService(PmsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<GroupReservationSummaryDTO>> GetAllGroupReservationsAsync()
    {
        var reservations = await _context.GroupReservations.ToListAsync();

        return _mapper.Map<IEnumerable<GroupReservation>, IEnumerable<GroupReservationSummaryDTO>>(
            reservations);
    }

    /// <inheritdoc />
    public async Task<GroupReservationDetailDTO?> FindGroupReservationAsync(int id)
    {
        var reservation = await _context.GroupReservations
            .Include(x => x.Reservations)
            .ThenInclude(x => x.ReservationDetails)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);

        return _mapper.Map<GroupReservationDetailDTO>(reservation);
    }

    /// <inheritdoc />
    public async Task<GroupReservationSummaryDTO> CreateGroupReservationAsync(
        CreateGroupReservationDTO reservation)
    {
        var entity = _mapper.Map<GroupReservation>(reservation);

        if (!await _context.AgencyContacts.AnyAsync(x => x.Id == entity.AgencyContactId))
        {
            throw new NotFoundException(
                $"Could not find Agency Contact with id {entity.AgencyContactId}");
        }

        await _context.GroupReservations.AddAsync(entity);
        await _context.SaveChangesAsync();

        return _mapper.Map<GroupReservationSummaryDTO>(entity);
    }

    /// <inheritdoc />
    public async Task<GroupReservationSummaryDTO> UpdateGroupReservationAsync(
        UpdateGroupReservationDTO reservation)
    {
        if (await _context.GroupReservations
                .Include(x => x.Reservations)
                .FirstOrDefaultAsync(x => x.Id == reservation.Id) is { } entity)
        {
            _mapper.Map(reservation, entity);

            if (!await _context.AgencyContacts.AnyAsync(x => x.Id == entity.AgencyContactId))
            {
                throw new NotFoundException(
                    $"Could not find Agency Contact with id {entity.AgencyContactId}");
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<GroupReservationSummaryDTO>(entity);
        }

        throw new NotFoundException("Could not find group reservation");
    }

    /// <inheritdoc />
    public async Task DeleteGroupReservationAsync(int id)
    {
        if (await _context.GroupReservations.FindAsync(id) is { } reservation)
        {
            _context.GroupReservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }
    }
}
