using FluentValidation;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input.Validators;

/// <summary>
/// Validator for properties of the <see cref="CreateGroupReservationDTO"/> class.
/// </summary>
public class CreateGroupReservationDTOValidator : AbstractValidator<CreateGroupReservationDTO>
{
    /// <summary>
    /// Initializes a new instance of <see cref="CreateGroupReservationDTOValidator"/>.
    /// </summary>
    public CreateGroupReservationDTOValidator()
    {
        RuleFor(x => x.Reference).MaximumLength(255);
        RuleFor(x => x.ReservationDate).NotNull();
        RuleFor(x => x.AgencyContactId).NotNull();
        RuleFor(x => x.Reservations).NotEmpty();
    }
}
