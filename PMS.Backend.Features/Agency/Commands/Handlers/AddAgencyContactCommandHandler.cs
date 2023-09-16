using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Agency.Commands.Payloads;
using PMS.Backend.Features.Agency.Entities;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Infrastructure;

namespace PMS.Backend.Features.Agency.Commands.Handlers;

[UsedImplicitly]
internal class AddAgencyContactCommandHandler(
        PmsContext context,
        IMapper mapper)
    : IRequestHandler<AddAgencyContactCommand, AddAgencyContactPayload>
{
    public async Task<AddAgencyContactPayload> Handle(AddAgencyContactCommand request, CancellationToken cancellationToken)
    {
        if (await context.Agencies
                .Include(entity => entity.Contacts)
                .FirstOrDefaultAsync(entity => entity.Id == request.AgencyId, cancellationToken)
            is not { } agency)
        {
            throw new NotFoundException<Entities.Agency>(request.AgencyId);
        }

        AgencyContact contact = CreateAgencyContact(request);

        agency.AddContact(contact);
        await context.SaveChangesAsync(cancellationToken);

        var contactModel = mapper.Map<Models.AgencyContact>(contact);
        return new AddAgencyContactPayload(contactModel);
    }

    private static AgencyContact CreateAgencyContact(AddAgencyContactCommand input)
        => new(
            input.Name,
            input.Email,
            input.Phone,
            input.Street,
            input.City,
            input.State,
            input.Country,
            input.ZipCode
        );
}
