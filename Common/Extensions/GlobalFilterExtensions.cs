using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq;

namespace Common.Extensions;

public static class GlobalFilterExtensions
{
    //public static EntityTypeBuilder HasQueryFilter<TSource>(this EntityTypeBuilder entityTypeBuilder,
    //                                                             Expression<Func<TSource, bool>> filterExpression)
    //{
    //    return entityTypeBuilder.HasQueryFilter(filterExpression.Convert(entityTypeBuilder.GetType()));
    //}

    public static EntityTypeBuilder HasQueryFilter<T>(this EntityTypeBuilder entityTypeBuilder, Expression<Func<T, bool>> filterExpression)
    {
        var param = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);
        var body = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), param, filterExpression.Body);

        var lambdaExp = Expression.Lambda(body, param);

        return entityTypeBuilder.HasQueryFilter(lambdaExp);
    }
}
