using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bdb.Specs.Sorters
{
	public class Sorter<TEntity>
	{
		protected Sorter(Expression<Func<TEntity, object>> firstOrderExpression)
		{
			_items = new Injecter<TEntity, object>[0];
			CreateAndAddSortItem(firstOrderExpression);
		}

		public Sorter<TEntity> ThenBy(Expression<Func<TEntity, object>> nextOrderExpression)
		{
			CreateAndAddSortItem(nextOrderExpression);
			return this;
		}

		public Sorter<TEntity> Desc()
		{
			var item = _items[_items.Length - 1];
			_items[_items.Length - 1] = new SorterItemDesc<TEntity>(item._expressionToInject);
			return this;
		}

		public TQueryable InjectTo<TQueryable>(TQueryable query)
			where TQueryable : class, IQueryable<TEntity>
		{
			for (int i = 0; i < _items.Length; i++)
				query = _items[i].InjectTo(query);
			return query;
		}

		private Injecter<TEntity, object>[] _items;

		private void CreateAndAddSortItem(Expression<Func<TEntity, object>> orderExpression)
		{
			Injecter<TEntity, object>[] newItems = new Injecter<TEntity, object>[_items.Length + 1];
			Array.Copy(_items, newItems, _items.Length);
			newItems[_items.Length] = new SorterItemAsc<TEntity>(orderExpression);
			_items = newItems;
		}

		#region static stuff (factory method)

		public static Sorter<TEntity> OrderBy(Expression<Func<TEntity, object>> orderExpression)
		{
			return new Sorter<TEntity>(orderExpression);
		}

		#endregion
	}
}
