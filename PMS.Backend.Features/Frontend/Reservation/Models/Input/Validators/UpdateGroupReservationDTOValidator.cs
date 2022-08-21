using FluentValidation;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input.Validators;

/// <summary>
/// Validator for properties of the <see cref="UpdateGroupReservationDTO"/> class.
/// </summary>
public class UpdateGroupReservationDTOValidator : AbstractValidator<UpdateGroupReservationDTO>
{
    /// <summary>
    /// Initializes a new <see cref="UpdateGroupReservationDTOValidator"/> instance.
    /// </summary>
    public UpdateGroupReservationDTOValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Reference).MaximumLength(255);
        RuleFor(x => x.Reference).MaximumLength(255);
        RuleFor(x => x.ReservationDate).NotNull();
        RuleFor(x => x.AgencyContactId).NotNull();
        RuleFor(x => x.Reservations).NotEmpty();
    }
}
