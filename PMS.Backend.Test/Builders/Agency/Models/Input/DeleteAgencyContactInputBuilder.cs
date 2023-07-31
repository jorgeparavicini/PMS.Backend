using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Test.Builders.Agency.Models.Input;

public class DeleteAgencyContactInputBuilder
{
    private int _id;

    public DeleteAgencyContactInputBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public DeleteAgencyContactInput Build()
    {
        return new DeleteAgencyContactInput
        {
            Id = _id,
        };
    }
}
