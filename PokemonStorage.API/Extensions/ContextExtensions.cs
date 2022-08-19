using Microsoft.EntityFrameworkCore;
using PokemonStorage.API.Data;
using System.Linq.Expressions;

namespace PokemonStorage.API.Extensions;

public static class ContextExtensions
{

    public static ICollection<T> Read<T>(this DbContext context, EntityQuery<T> query) where T : class => BuildQuery(context, query).ToList();
    public static T? FirstOrDefault<T>(this DbContext context, EntityQuery<T> query) where T : class => BuildQuery(context, query).FirstOrDefault();
    public static TEntity LoadCollection<TEntity, TProperty>(this DbContext context, TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> expression) where TEntity : class where TProperty : class
    {
        context.Entry(entity).Collection(expression).Load();
        return entity;
    }
    public static void Add<TEntity>(this DbContext context, TEntity entity) where TEntity : class => context.Entry(entity).State = EntityState.Added;

    private static IQueryable<T> BuildQuery<T>(DbContext context, EntityQuery<T> query) where T : class
    {
        IQueryable<T> queryable = context.Set<T>().AsQueryable();

        if (query.Taking > 0) { queryable.Take(query.Taking); }
        if (query.Skiping > 0) { queryable.Skip(query.Skiping); }
        foreach (Expression<Func<T, bool>> filter in query.Filters) { queryable.Where(filter); }
        foreach (Expression<Func<T, object>> include in query.Includes) { queryable.Include(include); }
        for (int i = 0; i < query.Sorts.Count; i++)
        {
            SortField<T> sort = query.Sorts.ToArray()[i];
            if (i == 0)
            {
                if (sort.DescendingSort) { queryable = queryable.OrderByDescending(sort.SortExpression); }
                else { queryable = queryable.OrderBy(sort.SortExpression); }
                continue;
            }

            if (sort.DescendingSort) { queryable = ((IOrderedQueryable<T>)queryable).ThenByDescending(sort.SortExpression); }
            else { queryable = ((IOrderedQueryable<T>)queryable).ThenBy(sort.SortExpression); }
        }

        return queryable;
    }
}
