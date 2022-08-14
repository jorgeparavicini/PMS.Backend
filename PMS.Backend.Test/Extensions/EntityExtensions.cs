using PMS.Backend.Core.Entities;
using PMS.Backend.Test.Assertions;

namespace PMS.Backend.Test.Extensions;

public static class EntityExtensions
{
    public static EntityAssertions<TEntity> Should<TEntity>(this TEntity? subject)
        where TEntity : Entity
    {
        return new EntityAssertions<TEntity>(subject);
    }
}
