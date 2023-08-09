using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.GraphQL.Reservation.Mutations;
using PMS.Backend.Features.GraphQL.Reservation.Queries;

namespace PMS.Backend.Features.GraphQL.Reservation.Extensions;

/// <summary>
///     Provides extension methods for <see cref="IRequestExecutorBuilder"/> to add the reservation GraphQL types.
/// </summary>
public static class RequestExecutorBuilderExtensions
{
    /// <summary>
    ///     Adds the reservation GraphQL types to the <see cref="IRequestExecutorBuilder"/>.
    /// </summary>
    /// <param name="builder">
    ///     The <see cref="IRequestExecutorBuilder"/> instance to add the reservation GraphQL types to.
    /// </param>
    /// <returns>
    ///     The <see cref="IRequestExecutorBuilder"/> instance with the added reservation GraphQL types.
    /// </returns>
    public static IRequestExecutorBuilder AddReservation(this IRequestExecutorBuilder builder)
    {
        return builder

            // Mutations
            .AddTypeExtension<CreateReservationMutation>()

            // Queries
            .AddTypeExtension<ReservationQuery>()
            .AddTypeExtension<ReservationsQuery>();
    }
}
