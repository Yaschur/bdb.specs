using System;
using System.Linq.Expressions;

namespace bdb.infra.specs.Specifications.ESpecifications
{
	public class NotESpecification<T> : ESpecification<T>
	{
		public NotESpecification(ESpecification<T> specificationToNegate)
		{
			SpecificationToNegate = specificationToNegate;
		}

		public override Expression<Func<T, bool>> TargetExpression
		{
			get
			{
				var expr = SpecificationToNegate.TargetExpression;
				return Expression.Lambda<Func<T, bool>>(
					Expression.Not(expr.Body),
					expr.Parameters
				);
			}
		}

		public ESpecification<T> SpecificationToNegate { get; protected set; }

		public override ISpecification<T> RemainderUnsatisfiedBy(T entity)
		{
			if (IsSatisfied(entity))
				return null;
			if (SpecificationToNegate is NotESpecification<T>)
			{
				return (SpecificationToNegate as NotESpecification<T>).SpecificationToNegate;
			}
			if (SpecificationToNegate is AndESpecification<T>)
			{
				var spec = SpecificationToNegate as AndESpecification<T>;
				return (spec.Components[0].Not())
					.Or(spec.Components[1].Not());
			}
			if (SpecificationToNegate is OrESpecification<T>)
			{
				var spec = SpecificationToNegate as OrESpecification<T>;
				return (spec.Components[0].Not())
					.And(spec.Components[1].Not());
			}
			return this;
		}

		//internal override ISpecification[] GetComponents()
		//{
		//	return new[] { SpecificationToNegate };
		//}
	}
}
