using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using PMS.Backend.Features.Agency.Commands;
using PMS.Backend.Features.Agency.Commands.Payloads;

namespace PMS.Backend.Api.GraphQL.Agency.Mutations;

[ExtendObjectType<Mutation>]
public class AddAgencyContactMutation
{
    public async Task<AddAgencyContactPayload> AddAgencyContactAsync(
        AddAgencyContactCommand input,
        [Service]
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        return await mediator.Send(input, cancellationToken);
    }
}
