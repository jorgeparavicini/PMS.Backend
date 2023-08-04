using System;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Test.Builders.Agency.Models.Input;

public class MoveAgencyContactToAgencyInputBuilder
{
    private Guid _agencyContactId;
    private Guid _agencyId;

    public MoveAgencyContactToAgencyInputBuilder WithAgencyContactId(Guid agencyContactId)
    {
        _agencyContactId = agencyContactId;
        return this;
    }

    public MoveAgencyContactToAgencyInputBuilder WithAgencyId(Guid agencyId)
    {
        _agencyId = agencyId;
        return this;
    }

    public MoveAgencyContactToAgencyInput Build()
    {
        return new MoveAgencyContactToAgencyInput
        {
            AgencyContactId = _agencyContactId,
            AgencyId = _agencyId,
        };
    }
}
