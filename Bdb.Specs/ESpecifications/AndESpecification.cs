using System;
using System.Linq.Expressions;

namespace Bdb.Specs.Specifications.ESpecifications
{
	public class AndESpecification<T> : CompositeESpecification<T>
	{
		public AndESpecification(ESpecification<T> leftSpecification, ESpecification<T> rightSpecification)
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
					Expression.AndAlso(nBodies[0], nBodies[1]),
					parameter
				);
			}
		}

		public override ISpecification<T> RemainderUnsatisfiedBy(T entity)
		{
			if (IsSatisfied(entity))
				return null;

			bool unsft1 = !Components[0].IsSatisfied(entity);
			bool unsft2 = !Components[1].IsSatisfied(entity);
			if (unsft1 && unsft2)
				return this;
			if (unsft1)
				return Components[0];
			return Components[1];
		}
	}
}
