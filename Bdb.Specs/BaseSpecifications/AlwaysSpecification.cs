using bdb.infra.specs.Specifications.ESpecifications;

namespace bdb.infra.specs.Specifications.BaseSpecifications
{
	public class AlwaysSpecification<TEntity> : StraightESpecification<TEntity>
	{
		public AlwaysSpecification()
			: base(e => true)
		{ }
	}
}
