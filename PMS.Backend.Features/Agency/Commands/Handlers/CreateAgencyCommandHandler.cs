using AutoMapper;
using MediatR;
using PMS.Backend.Features.Agency.Commands.Payloads;
using PMS.Backend.Features.Infrastructure;

namespace PMS.Backend.Features.Agency.Commands.Handlers;

internal class CreateAgencyCommandHandler(
        PmsContext context,
        IMapper mapper)
    : IRequestHandler<CreateAgencyCommand, CreateAgencyPayload>
{
    public async Task<CreateAgencyPayload> Handle(CreateAgencyCommand request, CancellationToken cancellationToken)
    {
        Entities.Agency agency = new(
            request.LegalName,
            request.DefaultCommission,
            request.DefaultCommissionOnExtras,
            (int)request.CommissionMethod,
            request.EmergencyContactEmail,
            request.EmergencyContactPhone,
            request.Contacts.Select(CreateAgencyContact).ToList()
        );

        await context.Agencies.AddAsync(agency, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        var agencyModel = mapper.Map<Models.Agency>(agency);
        return new CreateAgencyPayload(agencyModel);
    }

    private static Entities.AgencyContact CreateAgencyContact(CreateAgencyContactInput input)
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
