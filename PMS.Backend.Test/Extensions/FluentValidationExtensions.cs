using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PMS.Backend.Test.Extensions;

public static class FluentValidationExtensions
{
    public static string PathFor<TInput, TProperty>(
        this TInput input,
        Expression<Func<TInput, IEnumerable<TProperty>>> collectionExpression,
        Expression<Func<TProperty, object?>> expression)
    {
        var collection = collectionExpression.Body as MemberExpression;
        var member = expression.Body as MemberExpression;
        return $"{collection?.Member.Name}[0].{member?.Member.Name}";
    }
}
