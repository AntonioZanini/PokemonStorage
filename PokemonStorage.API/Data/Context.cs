using Microsoft.EntityFrameworkCore;
using PokemonStorage.API.Model;
using System.Linq.Expressions;

namespace PokemonStorage.API.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) 
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<Pokemon> Pokemons { get; set; } = null!;
    public DbSet<PokemonAbility> PokemonAbilities { get; set; } = null!;
    public DbSet<PokemonType> PokemonTypes { get; set; } = null!;

    public ICollection<T> Read<T>(EntityQuery<T> query) where T : class
    {
        IQueryable<T> queryable = Set<T>().AsQueryable();

        foreach(Expression<Func<T, bool>> filter in query.Filters) { queryable.Where(filter); }
        foreach(Expression<Func<T, object>> include in query.Includes) { queryable.Include(include); }
        for(int i = 0; i < query.Sorts.Count; i++)
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

        return queryable.ToList();
    }

}

