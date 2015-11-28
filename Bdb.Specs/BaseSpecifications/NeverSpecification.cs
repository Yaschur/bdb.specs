using Bdb.Specs.Specifications.ESpecifications;

namespace Bdb.Specs.Specifications.BaseSpecifications
{
	public class NeverSpecification<TEntity> : StraightESpecification<TEntity>
	{
		public NeverSpecification()
			: base(e => false)
		{ }
	}
}
