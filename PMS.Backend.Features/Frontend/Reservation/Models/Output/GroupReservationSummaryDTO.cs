using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Output;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record GroupReservationSummaryDTO(
    int Id,
    string? Reference,
    DateTime ReservationDate,
    bool IsQuote,
    int AgencyContactId);