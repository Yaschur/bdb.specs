using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bdb.Specs.Sorters
{
	internal class SorterItemDesc<TEntity> : Injecter<TEntity, object>
	{
		public SorterItemDesc(Expression<Func<TEntity, object>> sortExpression)
			: base(sortExpression)
		{ }

		public override TQueryable InjectTo<TQueryable>(TQueryable query)
		{
			// TODO: Ugly way. Review or use visitor
			if (query.Expression.Type == typeof(IOrderedQueryable<TEntity>))
				return (query as IOrderedQueryable<TEntity>).ThenByDescending(_expressionToInject) as TQueryable;
			return query.OrderByDescending(_expressionToInject) as TQueryable;
		}

	}
}
