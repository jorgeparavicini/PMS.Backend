using FluentValidation;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input.Validators;

/// <summary>
/// Validator for properties of the <see cref="UpdateReservationDTO"/> class.
/// </summary>
public class UpdateReservationDTOValidator : AbstractValidator<UpdateReservationDTO>
{
    /// <summary>
    /// Initializes a new <see cref="UpdateReservationDTOValidator"/> instance.
    /// </summary>
    public UpdateReservationDTOValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Name).MaximumLength(255);
        RuleFor(x => x.ReservationDetails).NotEmpty();
    }
}
