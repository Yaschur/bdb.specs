using System;
using System.Linq.Expressions;

namespace bdb.infra.specs.Specifications.ESpecifications
{
	public class OrESpecification<T> : CompositeESpecification<T>
	{
		public OrESpecification(ESpecification<T> leftSpecification, ESpecification<T> rightSpecification)
		{
			Components[0] = leftSpecification;
			Components[1] = rightSpecification;
		}

		public override Expression<Func<T, bool>> TargetExpression
		{
			get
			{
				var parameter = Expression.Parameter(typeof(T));
				var nBodies = GetNewBodies(parameter);

				return Expression.Lambda<Func<T, bool>>(
					Expression.OrElse(nBodies[0], nBodies[1]),
					parameter
				);
			}
		}

		public override ISpecification<T> RemainderUnsatisfiedBy(T entity)
		{
			if (IsSatisfied(entity))
				return null;
			return this;
		}
	}
}
