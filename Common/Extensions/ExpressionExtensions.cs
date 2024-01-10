using System.Linq.Expressions;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;

namespace Common.Extensions;

public static class ExpressionExtensions
{
    public static LambdaExpression Convert<TInterface>(this Expression<Func<TInterface, bool>> expression, Type targetType)
    {
        var newParameter = Expression.Parameter(targetType);
        var newBody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParameter, expression.Body);
        return Expression.Lambda(newBody, newParameter);
    }

    //class ParameterTypeVisitor<TSource, TTarget> : ExpressionVisitor
    //{
    //    private ReadOnlyCollection<ParameterExpression>? _parameters;

    //    protected override Expression VisitParameter(ParameterExpression node)
    //    {
    //        return _parameters?.FirstOrDefault(p => p.Name == node.Name)
    //          ?? (node.Type == typeof(TSource) ? Expression.Parameter(typeof(TTarget), node.Name) : node);
    //    }

    //    protected override Expression VisitLambda<T>(Expression<T> node)
    //    {
    //        _parameters = VisitAndConvert(node.Parameters, "VisitLambda");
    //        return Expression.Lambda(Visit(node.Body), _parameters);
    //    }
    //}

    //private static LambdaExpression ConvertFilterExpression<TInterface>(Expression<Func<TInterface, bool>> filterExpression, Type entityType)
    //{
    //    var newParam = Expression.Parameter(entityType);
    //    var newBody = ReplacingExpressionVisitor.Replace(filterExpression.Parameters.Single(), newParam, filterExpression.Body);

    //    return Expression.Lambda(newBody, newParam);
    //}

    //builder.Model.GetEntityTypes().Where(entityType => typeof(IOwnableEntity).IsAssignableFrom(entityType.ClrType)).ToList().ForEach(entityType =>
    //    {
    //    var parameter = Expression.Parameter(entityType.ClrType);
    //    Expression<Func<IOwnableEntity, bool>> expression = (x => EstablishmentId == null || x.EstablishmentId == EstablishmentId);
    //    var newBody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), parameter, expression.Body);
    //    builder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(newBody, parameter));
    //});
}
