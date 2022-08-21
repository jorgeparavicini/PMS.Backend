using FluentValidation;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input.Validators;

/// <summary>
/// Validator for properties of the <see cref="UpdateReservationDetailDTO"/> class.
/// </summary>
public class UpdateReservationDetailDTOValidator : AbstractValidator<UpdateReservationDetailDTO>
{
    /// <summary>
    /// Initializes a new <see cref="UpdateReservationDetailDTOValidator"/> instance.
    /// </summary>
    public UpdateReservationDetailDTOValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.ReservationDate).NotNull();
        RuleFor(x => x.CheckIn).NotNull();
        RuleFor(x => x.CheckOut).NotNull();
    }
}
