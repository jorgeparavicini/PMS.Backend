using PMS.Backend.Features.Frontend.Reservation.Models.Input;
using PMS.Backend.Features.Frontend.Reservation.Models.Output;

namespace PMS.Backend.Features.Frontend.Reservation.Services.Contracts;

public interface IReservationService
{
    Task<IEnumerable<GroupReservationSummaryDTO>> GetAllGroupReservationsAsync();

    Task<GroupReservationDetailDTO?> FindGroupReservationAsync(int id);

    Task<GroupReservationSummaryDTO> CreateGroupReservationAsync(
        CreateGroupReservationDTO reservation);

    Task<GroupReservationSummaryDTO> UpdateGroupReservationAsync(
        UpdateGroupReservationDTO reservation);

    Task DeleteGroupReservationAsync(int id);
}