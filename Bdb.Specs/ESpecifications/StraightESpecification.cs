using System;
using System.Linq.Expressions;

namespace bdb.infra.specs.Specifications.ESpecifications
{
	public class StraightESpecification<T> : LeafESpecification<T>
	{
		public StraightESpecification(Expression<Func<T, bool>> targetExpression)
		{
			_tmpExpr = targetExpression;
		}

		protected override Expression<Func<T, bool>> BuildExpression()
		{
			return _tmpExpr;
		}

		private Expression<Func<T, bool>> _tmpExpr;
	}
}
