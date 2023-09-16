﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Agency.Commands.Payloads;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Infrastructure;

namespace PMS.Backend.Features.Agency.Commands.Handlers;

internal class EditAgencyCommandHandler(
        PmsContext context,
        IMapper mapper)
    : IRequestHandler<EditAgencyCommand, EditAgencyPayload>
{
    public async Task<EditAgencyPayload> Handle(EditAgencyCommand request, CancellationToken cancellationToken)
    {
        if (await context.Agencies
                .Include(entity => entity.Contacts)
                .FirstOrDefaultAsync(entity => entity.Id == request.Id, cancellationToken)
            is not { } agency)
        {
            throw new NotFoundException<Entities.Agency>(request.Id);
        }

        agency.SetAgencyDetails(
            request.LegalName,
            request.DefaultCommission,
            request.DefaultCommissionOnExtras,
            (int)request.CommissionMethod,
            request.EmergencyContactEmail,
            request.EmergencyContactPhone);

        await context.SaveChangesAsync(cancellationToken);

        var agencyModel = mapper.Map<Models.Agency>(agency);

        return new EditAgencyPayload(agencyModel);
    }
}
