
namespace bdb.infra.specs.Specifications.BaseSpecifications
{
	public static class Extensions
	{
		public static ISpecification<T> AndStartIfNull<T>(this ISpecification<T> target, ISpecification<T> specification)
		{
			return target == null ? specification : target.And(specification);
		}

		public static ISpecification<T> OrStartIfNull<T>(this ISpecification<T> target, ISpecification<T> specification)
		{
			return target == null ? specification : target.Or(specification);
		}
	}
}
