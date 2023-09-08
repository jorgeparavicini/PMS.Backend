using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Agency.Models;

namespace PMS.Backend.Api.GraphQL.Agency.Types;
/*
[ExtendObjectType<AgencyContact>]
public class AgencyContactNode
{
    public async Task<Features.Agency.Models.Agency> GetAgencyAsync(
        [Parent]
        AgencyContact agencyContact,
        IAgencyByIdDataLoader agencyById,
        CancellationToken cancellationToken)
        => await agencyById.LoadAsync(agencyContact.AgencyId, cancellationToken);

    [DataLoader]
    internal static async Task<IReadOnlyDictionary<Guid, AgencyContact>> GetAgencyContactByIdAsync(
        IReadOnlyList<Guid> ids,
        PmsContext context,
        [Service]
        IMapper mapper,
        CancellationToken cancellationToken)
    {
        // TODO: We shouldn't use a db context here
        return await context.AgencyContacts
            .Where(contact => ids.Contains(contact.Id))
            .ProjectTo<AgencyContact>(mapper.ConfigurationProvider)
            .ToDictionaryAsync(contact => contact.Id, cancellationToken: cancellationToken);
    }
}
*/