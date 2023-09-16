using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Agency.Commands.Payloads;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Infrastructure;

namespace PMS.Backend.Features.Agency.Commands.Handlers;

internal class DeleteAgencyContactCommandHandler(
        PmsContext context,
        IMapper mapper)
    : IRequestHandler<DeleteAgencyContactCommand, DeleteAgencyContactPayload>
{
    public async Task<DeleteAgencyContactPayload> Handle(DeleteAgencyContactCommand request, CancellationToken cancellationToken)
    {
        if (await context.Agencies
                .Include(entity => entity.Contacts)
                .FirstOrDefaultAsync(entity => entity.Id == request.AgencyId, cancellationToken)
            is not { } agency)
        {
            throw new NotFoundException<Entities.Agency>(request.AgencyId);
        }

        agency.DeleteContact(request.ContactId);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteAgencyContactPayload();
    }
}
