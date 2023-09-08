using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Agency.Models.Input;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Infrastructure;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Agency.Commands.Handlers;

internal class EditAgencyCommandHandler(
        PmsContext context,
        IMapper mapper)
    : IRequestHandler<EditAgencyCommand, Models.Agency>
{
    public async Task<Models.Agency> Handle(EditAgencyCommand request, CancellationToken cancellationToken)
    {
        EditAgencyInput input = request.Input;

        if (await context.Agencies
                .Include(entity => entity.Contacts)
                .FirstOrDefaultAsync(entity => entity.Id == input.Id, cancellationToken)
            is not { } agency)
        {
            throw new NotFoundException<Entities.Agency>(input.Id);
        }

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

        agency.SetAgencyDetails(
            input.LegalName,
            defaultCommission,
            defaultCommissionOnExtras,
            commissionMethod,
            contactDetails);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<Models.Agency>(agency);
    }
}
