using System;
using System.Linq.Expressions;

namespace bdb.infra.specs.Specifications.ESpecifications
{
	public abstract class LeafESpecification<T> : ESpecification<T>
	{
		public LeafESpecification()
		{
			_trgExpr = new Lazy<Expression<Func<T, bool>>>(BuildExpression, true);
		}

		public override Expression<Func<T, bool>> TargetExpression
		{
			get { return _trgExpr.Value; }
		}

		public override ISpecification<T> RemainderUnsatisfiedBy(T entity)
		{
			if (IsSatisfied(entity))
				return null;
			return this;
		}

		//internal override ISpecification[] GetComponents()
		//{
		//	return new ISpecification[0];
		//}

		protected abstract Expression<Func<T, bool>> BuildExpression();

		private Lazy<Expression<Func<T, bool>>> _trgExpr;
	}
}
