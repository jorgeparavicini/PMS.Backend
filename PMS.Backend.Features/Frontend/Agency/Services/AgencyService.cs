using PMS.Backend.Core.Database;
using PMS.Backend.Features.Common;

namespace PMS.Backend.Features.Frontend.Agency.Services;

/// <inheritdoc/>
public class AgencyService : Service<Core.Entities.Agency.Agency>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AgencyService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public AgencyService(PmsDbContext context) : base(context)
    {
    }
}
