using Bdb.Specs.Specifications.ESpecifications;

namespace Bdb.Specs.Specifications.BaseSpecifications
{
	public class AlwaysSpecification<TEntity> : StraightESpecification<TEntity>
	{
		public AlwaysSpecification()
			: base(e => true)
		{ }
	}
}
