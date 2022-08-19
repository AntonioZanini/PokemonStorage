using System.Linq.Expressions;

namespace PokemonStorage.API.Data;

public class EntityQuery<TEntity> where TEntity : class
{
	public ICollection<Expression<Func<TEntity, object>>> Includes { get; private set; }
	public ICollection<Expression<Func<TEntity, bool>>> Filters { get; private set; }
	public ICollection<SortField<TEntity>> Sorts { get; private set; }
	public int Taking { get; private set; }
	public int Skiping { get; private set; }
	public EntityQuery()
	{
		Includes = new List<Expression<Func<TEntity, object>>>();
		Filters = new List<Expression<Func<TEntity, bool>>>();
		Sorts = new List<SortField<TEntity>>();
	}

	public EntityQuery<TEntity> Take(int value)
	{
		Taking = value;
		return this;
	}
	public EntityQuery<TEntity> Skip(int value)
	{
		Skiping = value;
		return this;
	}

	public EntityQuery<TEntity> Filter(Expression<Func<TEntity, bool>> filter)
	{
		Filters.Add(filter);
		return this;
	}

	public EntityQuery<TEntity> Load(Expression<Func<TEntity, object>> include)
	{
		Includes.Add(include);
		return this;
	}

	public EntityQuery<TEntity> Sort(Expression<Func<TEntity, object>> sortExpression, bool descendingSort = false)
	{
		Sorts.Add(new SortField<TEntity>(sortExpression, descendingSort));
		return this;
	}


}
