using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using PMS.Backend.Core.Entities.Agency;

namespace PMS.Backend.Service.Extensions;

/// <summary>
/// Extensions for <see cref="DbContext"/> instances.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Generates a <see cref="EdmModel"/> from a given <see cref="DbContext"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the <see cref="DbContext"/> to generate the model from.
    /// </typeparam>
    /// <returns>An <see cref="EdmModel"/> containing all <see cref="DbSet{TEntity}"/>.</returns>
    public static IEdmModel GetModel<T>()
        where T : DbContext
    {
        var builder = new ODataConventionModelBuilder();
        builder.EntitySet<Agency>("Agencies");
        builder.EntitySet<AgencyContact>("AgencyContacts");

        return builder.GetEdmModel();
    }
}
