using FluentValidation;
using PMS.Backend.Features.GraphQL.Reservation.Models.Input;

namespace PMS.Backend.Features.GraphQL.Reservation.Validation;

/// <summary>
///     Validator for <see cref="CreateReservationReservationDetailsInput"/>.
/// </summary>
public class CreateReservationReservationDetailInputValidator
    : AbstractValidator<CreateReservationReservationDetailsInput>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateReservationReservationDetailInputValidator"/> class.
    /// </summary>
    public CreateReservationReservationDetailInputValidator()
    {
        RuleFor(reservationDetail => reservationDetail.CheckIn)
            .NotEmpty()
            .LessThanOrEqualTo(reservationDetail => reservationDetail.CheckOut)
            .WithMessage("CheckIn must be before CheckOut.");

        RuleFor(reservationDetail => reservationDetail.CheckOut).NotEmpty();
    }
}
