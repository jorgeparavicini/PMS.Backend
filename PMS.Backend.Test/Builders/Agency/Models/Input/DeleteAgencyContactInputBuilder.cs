using System;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Test.Builders.Agency.Models.Input;

public class DeleteAgencyContactInputBuilder
{
    private Guid _id;

    public DeleteAgencyContactInputBuilder WithId(Guid id)
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
