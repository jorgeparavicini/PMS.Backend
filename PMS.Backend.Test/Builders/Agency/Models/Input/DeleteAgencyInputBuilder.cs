using System;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Test.Builders.Agency.Models.Input;

public class DeleteAgencyInputBuilder
{
    private Guid _id;

    public DeleteAgencyInputBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public DeleteAgencyInput Build()
    {
        return new DeleteAgencyInput
        {
            Id = _id,
        };
    }
}
