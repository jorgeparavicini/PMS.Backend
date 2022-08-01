using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using PMS.Backend.Core.Entities.Agency;

namespace PMS.Backend.Service.Extensions;

public static class DbContextExtensions
{
    public static IEdmModel GetModel<T>()
        where T : DbContext
    {
        var builder = new ODataConventionModelBuilder();
        builder.EntitySet<Agency>("Agencies");
        builder.EntitySet<AgencyContact>("AgencyContacts");

        return builder.GetEdmModel();
    }
}
