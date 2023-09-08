using AutoMapper;
using MediatR;
using PMS.Backend.Features.Agency.Models.Input;
using PMS.Backend.Features.Infrastructure;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Agency.Commands.Handlers;

internal class CreateAgencyCommandHandler(
        PmsContext context,
        IMapper mapper)
    : IRequestHandler<CreateAgencyCommand, Models.Agency>
{
    public async Task<Models.Agency> Handle(CreateAgencyCommand request, CancellationToken cancellationToken)
    {
        CreateAgencyInput input = request.Input;
        Commission? defaultCommission = input.DefaultCommission.HasValue
            ? new Commission(input.DefaultCommission.Value)
            : null;

        Commission? defaultCommissionOnExtras = input.DefaultCommissionOnExtras.HasValue
            ? new Commission(input.DefaultCommissionOnExtras.Value)
            : null;

        CommissionMethod commissionMethod = CommissionMethod.FromName(input.CommissionMethod);

        Email? email = string.IsNullOrWhiteSpace(input.EmergencyContactEmail)
            ? null
            : new Email(input.EmergencyContactEmail);

        Phone? phone = string.IsNullOrWhiteSpace(input.EmergencyContactPhone)
            ? null
            : new Phone(input.EmergencyContactPhone);

        ContactDetails contactDetails = new(email, phone);

        Entities.Agency agency = new(
            input.LegalName,
            defaultCommission,
            defaultCommissionOnExtras,
            commissionMethod,
            contactDetails,
            input.Contacts.Select(CreateAgencyContact).ToList()
        );

        await context.Agencies.AddAsync(agency, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<Models.Agency>(agency);
    }

    private static Entities.AgencyContact CreateAgencyContact(CreateAgencyContactInput input)
    {
        Email? email = string.IsNullOrWhiteSpace(input.Email)
            ? null
            : new Email(input.Email);

        Phone? phone = string.IsNullOrWhiteSpace(input.Phone)
            ? null
            : new Phone(input.Phone);

        ContactDetails contactDetails = new(email, phone);

        Address address = new(input.Street, input.City, input.State, input.Country, input.ZipCode);

        return new Entities.AgencyContact(
            input.Name,
            contactDetails,
            address
        );
    }
}
