using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Agency.Commands.Payloads;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Infrastructure;

namespace PMS.Backend.Features.Agency.Commands.Handlers;

internal class DeleteAgencyCommandHandler(
        PmsContext context,
        IMapper mapper)
    : IRequestHandler<DeleteAgencyCommand, DeleteAgencyPayload>
{
    public async Task<DeleteAgencyPayload> Handle(DeleteAgencyCommand request, CancellationToken cancellationToken)
    {
        if (await context.Agencies
                .Include(entity => entity.Contacts)
                .FirstOrDefaultAsync(entity => entity.Id == request.Id, cancellationToken)
            is not { } agency)
        {
            throw new NotFoundException<Entities.Agency>(request.Id);
        }

        agency.Delete();
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteAgencyPayload();
    }
}
