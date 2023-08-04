using System;
using FluentValidation.TestHelper;
using PMS.Backend.Features.GraphQL.Reservation.Models.Input;
using PMS.Backend.Features.GraphQL.Reservation.Validation;
using PMS.Backend.Test.Builders.Reservation.Models;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Reservation.Validation;

[UnitTest]
public class CreateReservationInputValidator
{
    private readonly CreateReservationGroupReservationInputValidator _sut = new();

    public static TheoryData<DateOnly, DateOnly> ValidCheckInDates => new()
    {
        // TODO: Is this valid? Should we allow check-in and check-out on the same day?
        { DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today) },
        { DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(1)) },
    };

    public static TheoryData<DateOnly, DateOnly> InvalidCheckInDates => new()
    {
        { DateOnly.FromDateTime(DateTime.Today.AddDays(1)), DateOnly.FromDateTime(DateTime.Today) },
        { DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today.AddDays(-1)) },
    };

    [Fact]
    public void Validate_ShouldSucceed_WhenMinimalInput()
    {
        // Arrange
        CreateReservationGroupReservationInput input = new CreateReservationGroupReservationInputBuilder().Build();

        // Act
        TestValidationResult<CreateReservationGroupReservationInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldFail_WhenReferenceIsTooLong()
    {
        // Arrange
        CreateReservationGroupReservationInput input = new CreateReservationGroupReservationInputBuilder()
            .WithReference(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<CreateReservationGroupReservationInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(reservation => reservation.Reference);
    }

    [Fact]
    public void Validate_ShouldFail_WhenAgencyContactIdIsInvalid()
    {
        // Arrange
        CreateReservationGroupReservationInput input = new CreateReservationGroupReservationInputBuilder()
            .WithAgencyContactId(Guid.Empty)
            .Build();

        // Act
        TestValidationResult<CreateReservationGroupReservationInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(reservation => reservation.AgencyContactId);
    }

    [Fact]
    public void Validate_ShouldFail_WhenReservationsIsEmpty()
    {
        // Arrange
        CreateReservationGroupReservationInput input = new CreateReservationGroupReservationInputBuilder()
            .WithReservations()
            .Build();

        // Act
        TestValidationResult<CreateReservationGroupReservationInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(reservation => reservation.Reservations);
    }

    [Fact]
    public void Validate_ShouldFail_WhenReservationNameIsTooLong()
    {
        // Arrange
        CreateReservationGroupReservationInput input = new CreateReservationGroupReservationInputBuilder()
            .WithReservations(builder => builder
                .WithName(new string('a', 256))
                .Build())
            .Build();

        // Act
        TestValidationResult<CreateReservationGroupReservationInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor("Reservations[0].Name");
    }

    [Fact]
    public void Validate_ShouldFail_WhenReservationDetailsIsEmpty()
    {
        // Arrange
        CreateReservationGroupReservationInput input = new CreateReservationGroupReservationInputBuilder()
            .WithReservations(builder => builder
                .WithReservationDetails()
                .Build())
            .Build();

        // Act
        TestValidationResult<CreateReservationGroupReservationInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor("Reservations[0].ReservationDetails");
    }

    [Theory]
    [MemberData(nameof(ValidCheckInDates))]
    public void Validate_ShouldSucceed_WhenValidCheckInAndCheckOutDates(DateOnly checkIn, DateOnly checkOut)
    {
        // Arrange
        CreateReservationGroupReservationInput input = new CreateReservationGroupReservationInputBuilder()
            .WithReservations(builder => builder
                .WithReservationDetails(detailsBuilder => detailsBuilder
                    .WithCheckIn(checkIn)
                    .WithCheckOut(checkOut)
                    .Build())
                .Build())
            .Build();

        // Act
        TestValidationResult<CreateReservationGroupReservationInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [MemberData(nameof(InvalidCheckInDates))]
    public void Validate_ShouldFail_WhenInvalidCheckInAndCheckOutDates(DateOnly checkIn, DateOnly checkOut)
    {
        // Arrange
        CreateReservationGroupReservationInput input = new CreateReservationGroupReservationInputBuilder()
            .WithReservations(builder => builder
                .WithReservationDetails(detailsBuilder => detailsBuilder
                    .WithCheckIn(checkIn)
                    .WithCheckOut(checkOut)
                    .Build())
                .Build())
            .Build();

        // Act
        TestValidationResult<CreateReservationGroupReservationInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor("Reservations[0].ReservationDetails[0].CheckIn");
    }
}
