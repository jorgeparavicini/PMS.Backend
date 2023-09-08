using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using PMS.Backend.Features.Shared;

namespace PMS.Backend.Features.Extensions;

public static class ModelBuilderExtensions
{
    // TODO: Could be improved by not looping over all entities each time.

    public static ModelBuilder AddSoftDeleteQueryFilter(this ModelBuilder modelBuilder)
    {
        Expression<Func<Entity, bool>> filterExpr = e => !e.IsDeleted;

        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(Entity).IsAssignableFrom(entityType.ClrType))
            {
                continue;
            }

            ParameterExpression parameter = Expression.Parameter(entityType.ClrType, "p");
            Expression body = ReplacingExpressionVisitor.Replace(
                filterExpr.Parameters.Single(),
                parameter,
                filterExpr.Body);
            LambdaExpression filter = Expression.Lambda(body, parameter);
            entityType.SetQueryFilter(filter);
        }

        return modelBuilder;
    }
}
