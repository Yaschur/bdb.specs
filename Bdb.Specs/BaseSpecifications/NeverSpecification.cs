using bdb.infra.specs.Specifications.ESpecifications;

namespace nqt.roots.infra.Specifications.BaseSpecifications
{
	public class NeverSpecification<TEntity> : StraightESpecification<TEntity>
	{
		public NeverSpecification()
			: base(e => false)
		{ }
	}
}
