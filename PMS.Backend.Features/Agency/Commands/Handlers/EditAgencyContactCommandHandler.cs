using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Agency.Commands.Payloads;
using PMS.Backend.Features.Agency.Models;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Infrastructure;

namespace PMS.Backend.Features.Agency.Commands.Handlers;

internal class EditAgencyContactCommandHandler(
        PmsContext context,
        IMapper mapper)
    : IRequestHandler<EditAgencyContactCommand, EditAgencyContactPayload>
{
    public async Task<EditAgencyContactPayload> Handle(
        EditAgencyContactCommand request,
        CancellationToken cancellationToken)
    {
        if (await context.Agencies
                .Include(entity => entity.Contacts)
                .FirstOrDefaultAsync(entity => entity.Id == request.AgencyId, cancellationToken)
            is not { } agency)
        {
            throw new NotFoundException<Entities.Agency>(request.AgencyId);
        }

        agency.UpdateContact(request.Id,
            request.Name,
            request.Email,
            request.Phone,
            request.Street,
            request.City,
            request.State,
            request.Country,
            request.ZipCode);
        await context.SaveChangesAsync(cancellationToken);

        var contactModel = mapper.Map<AgencyContact>(agency.Contacts.First(contact => contact.Id == request.Id));

        return new EditAgencyContactPayload(contactModel);
    }
}
