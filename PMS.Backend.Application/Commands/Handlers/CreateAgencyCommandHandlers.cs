using AutoMapper;
using MediatR;
using PMS.Backend.Application.Models.Agency.Input;
using PMS.Backend.Domain.Aggregates.Agency;
using PMS.Backend.Domain.ValueObjects;
using PMS.Backend.Persistence.Db;

namespace PMS.Backend.Application.Commands.Handlers;

public class CreateAgencyCommandHandlers(
        PmsContext context,
        IMapper mapper)
    : IRequestHandler<CreateAgencyCommand, Models.Agency.Agency>
{
    public async Task<Models.Agency.Agency> Handle(CreateAgencyCommand request, CancellationToken cancellationToken)
    {
        CreateAgencyInput input = request.Input;
        Commission? defaultCommission = input.DefaultCommission.HasValue
            ? new Commission(input.DefaultCommission.Value)
            : null;

        Commission? defaultCommissionOnExtras = input.DefaultCommissionOnExtras.HasValue
            ? new Commission(input.DefaultCommissionOnExtras.Value)
            : null;

        CommissionMethod commissionMethod = CommissionMethod.From(input.CommissionMethod);

        Email? email = string.IsNullOrWhiteSpace(input.EmergencyContactEmail)
            ? null
            : new Email(input.EmergencyContactEmail);

        Phone? phone = string.IsNullOrWhiteSpace(input.EmergencyContactPhone)
            ? null
            : new Phone(input.EmergencyContactPhone);

        ContactDetails contactDetails = new(email, phone);

        Agency agency = new(
            input.LegalName,
            defaultCommission,
            defaultCommissionOnExtras,
            commissionMethod,
            contactDetails
        );

        await context.Agencies.AddAsync(agency, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<Models.Agency.Agency>(agency);
    }
}
