using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Agency.Entities;
using PMS.Backend.Features.Agency.Models.Input;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Infrastructure;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Agency.Commands.Handlers;

[UsedImplicitly]
internal class CreateAgencyContactCommandHandler(
    PmsContext context,
    IMapper mapper)
: IRequestHandler<CreateAgencyContactCommand, Models.Agency>
{
    public async Task<Models.Agency> Handle(CreateAgencyContactCommand request, CancellationToken cancellationToken)
    {
        if (await context.Agencies
                .Include(entity => entity.Contacts)
                .FirstOrDefaultAsync(entity => entity.Id == request.AgencyId, cancellationToken)
            is not { } agency)
        {
            throw new NotFoundException<Entities.Agency>(request.AgencyId);
        }

        AgencyContact contact = CreateAgencyContact(request.Input);
        
        agency.AddContact(contact);
        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<Models.Agency>(agency);
    }
    
    private static AgencyContact CreateAgencyContact(CreateAgencyContactInput input)
    {
        Email? email = string.IsNullOrWhiteSpace(input.Email)
            ? null
            : new Email(input.Email);

        Phone? phone = string.IsNullOrWhiteSpace(input.Phone)
            ? null
            : new Phone(input.Phone);

        ContactDetails contactDetails = new(email, phone);

        Address address = new(input.Street, input.City, input.State, input.Country, input.ZipCode);

        return new AgencyContact(
            input.Name,
            contactDetails,
            address
        );
    }
}
