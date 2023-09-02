using System.Collections.Generic;
using System.Threading.Tasks;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Test.Builders.Reservation.Entity;

namespace PMS.Backend.Test.Fixtures.Reservation;

public class ReservationDatabaseFixture : DatabaseFixture
{
    protected IEnumerable<GroupReservation> Entities { get; } = new[]
    {
        new GroupReservationBuilder()
            .WithReference("Reference 1")
            .WithIsQuote(true)
            .WithReservations(
                builder => builder.WithReservationDetails(_ => { }, _ => { }),
                _ => { })
            .Build(),
        new GroupReservationBuilder().Build(),
    };

    protected override async Task SeedDatabase()
    {
        DbContext.GroupReservations.AddRange(Entities);
        await DbContext.SaveChangesAsync();
    }
}
