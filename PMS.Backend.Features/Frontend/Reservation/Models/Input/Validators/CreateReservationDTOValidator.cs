using FluentValidation;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input.Validators;

/// <summary>
/// Validator for properties of the <see cref="CreateReservationDTO"/> class.
/// </summary>
public class CreateReservationDTOValidator : AbstractValidator<CreateReservationDTO>
{
    /// <summary>
    /// Initializes a new <see cref="CreateReservationDTOValidator"/> instance.
    /// </summary>
    public CreateReservationDTOValidator()
    {
        RuleFor(x => x.Name).MaximumLength(255);
        RuleFor(x => x.ReservationDetails).NotEmpty();
    }
}
