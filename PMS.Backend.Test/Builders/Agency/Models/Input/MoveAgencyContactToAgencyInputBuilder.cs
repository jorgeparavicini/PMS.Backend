using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Test.Builders.Agency.Models.Input;

public class MoveAgencyContactToAgencyInputBuilder
{
    private int _agencyContactId;
    private int _agencyId;

    public MoveAgencyContactToAgencyInputBuilder WithAgencyContactId(int agencyContactId)
    {
        _agencyContactId = agencyContactId;
        return this;
    }

    public MoveAgencyContactToAgencyInputBuilder WithAgencyId(int agencyId)
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
