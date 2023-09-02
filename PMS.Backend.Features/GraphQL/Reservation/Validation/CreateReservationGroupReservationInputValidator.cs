using FluentValidation;
using PMS.Backend.Features.GraphQL.Reservation.Models.Input;

namespace PMS.Backend.Features.GraphQL.Reservation.Validation;

/// <summary>
///     Validator for <see cref="CreateReservationGroupReservationInput"/>.
/// </summary>
public class CreateReservationGroupReservationInputValidator
    : AbstractValidator<CreateReservationGroupReservationInput>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateReservationGroupReservationInputValidator"/> class.
    /// </summary>
    public CreateReservationGroupReservationInputValidator()
    {
        RuleFor(reservation => reservation.Reference).MaximumLength(255);

        RuleFor(reservation => reservation.AgencyContactId).NotEmpty();

        RuleFor(reservation => reservation.Reservations).NotEmpty();

        RuleForEach(reservation => reservation.Reservations)
            .SetValidator(new CreateReservationReservationInputValidator());
    }
}
