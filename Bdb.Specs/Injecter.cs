using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bdb.Specs
{
	internal abstract class Injecter<TEntity, TProperty>
	{
		public Injecter(Expression<Func<TEntity, TProperty>> injectingExpression)
		{
			_expressionToInject = injectingExpression;
		}

		public abstract TQueryable InjectTo<TQueryable>(TQueryable query) where TQueryable : class, IQueryable<TEntity>;

		internal readonly Expression<Func<TEntity, TProperty>> _expressionToInject;
	}
}
