namespace bdb.infra.specs
{
	public interface ISpecification
	{
		bool IsSatisfied(object entity);
	}

	public interface ISpecification<T> : ISpecification
	{
		bool IsSatisfied(T entity);
		ISpecification<T> And(ISpecification<T> specification);
		ISpecification<T> Or(ISpecification<T> specification);
		ISpecification<T> RemainderUnsatisfiedBy(T entity);
		ISpecification<T> Not();
	}
}
