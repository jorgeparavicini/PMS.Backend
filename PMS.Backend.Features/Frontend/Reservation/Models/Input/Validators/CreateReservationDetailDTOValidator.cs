using FluentValidation;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input.Validators;

/// <summary>
/// Validator for properties of the <see cref="CreateReservationDetailDTO"/> class.
/// </summary>
public class CreateReservationDetailDTOValidator
    : AbstractValidator<CreateReservationDetailDTO>
{
    /// <summary>
    /// Initializes a new <see cref="CreateReservationDetailDTOValidator"/> instance.
    /// </summary>
    public CreateReservationDetailDTOValidator()
    {
        RuleFor(x => x.ReservationDate).NotNull();
        RuleFor(x => x.CheckIn).NotNull();
        RuleFor(x => x.CheckOut).NotNull();
    }
}
