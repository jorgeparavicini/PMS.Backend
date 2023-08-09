using FluentValidation;
using PMS.Backend.Features.GraphQL.Reservation.Models.Input;

namespace PMS.Backend.Features.GraphQL.Reservation.Validation;

/// <summary>
///     Validator for <see cref="CreateReservationReservationInput"/>.
/// </summary>
public class CreateReservationReservationInputValidator
    : AbstractValidator<CreateReservationReservationInput>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateReservationReservationInputValidator"/> class.
    /// </summary>
    public CreateReservationReservationInputValidator()
    {
        RuleFor(reservation => reservation.Name).MaximumLength(255);

        RuleFor(reservation => reservation.ReservationDetails).NotEmpty();

        RuleForEach(reservation => reservation.ReservationDetails)
            .SetValidator(new CreateReservationReservationDetailInputValidator());
    }
}
