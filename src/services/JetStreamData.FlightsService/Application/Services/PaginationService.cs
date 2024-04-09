using System.Linq.Expressions;
using System.Reflection;
using JetStreamData.Kernel.Application.Model;

namespace JetStreamData.FlightsService.Application.Services;

public static class PaginationService
{
    public static IQueryable<TEntity> ApplyPaginationQuery<TEntity, TResponse>(
        this IQueryable<TEntity> query, BasePaginationQuery<TResponse> paginationQuery)
        where TEntity : class
    {
        if (!string.IsNullOrEmpty(paginationQuery.OrderBy) && PropertyExists<TEntity>(paginationQuery.OrderBy))
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var selector = Expression.PropertyOrField(parameter, paginationQuery.OrderBy);
            var lambda = Expression.Lambda(selector, parameter);

            query = paginationQuery.Direction switch
            {
                "asc" => Queryable.OrderBy(query, (dynamic)lambda),
                "desc" => Queryable.OrderByDescending(query, (dynamic)lambda),
                _ => query
            };
        }

        var skip = paginationQuery.PageSize * (paginationQuery.Page - 1);
        query = query.Skip(skip).Take(paginationQuery.PageSize);

        return query;
    }

    private static bool PropertyExists<T>(string propertyName)
    {
        return typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) != null;
    }
}
