using bdb.infra.specs.Specifications.ESpecifications;
using System;
using System.Linq.Expressions;

namespace bdb.infra.specs.Extensions
{
	public static class SpecificationToExpressionExtension
	{
		public static Expression<Func<TEntity, bool>> Map2Expression<TEntity>(this ISpecification<TEntity> specification)
		{
			if (specification == null)
				return null;
			if (specification is ESpecification<TEntity>)
				return ((ESpecification<TEntity>)specification).TargetExpression;

			return entity => specification.IsSatisfied(entity);

			//string eMessage = string.Format("Specification is not supported: {0}", specification.GetType().FullName);
			//throw new NotSupportedException(eMessage);
		}
	}
}
