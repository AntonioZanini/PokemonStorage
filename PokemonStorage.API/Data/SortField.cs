using System.Linq.Expressions;

namespace PokemonStorage.API.Data;

public class SortField<TEntity> where TEntity : class
{
    public Expression<Func<TEntity, object>> SortExpression { get; private set; }
    public bool DescendingSort { get; private set; }

    public SortField(Expression<Func<TEntity, object>> sortExpression, bool descendingSort)
    {
        SortExpression = sortExpression;
        DescendingSort = descendingSort;
    }
}
