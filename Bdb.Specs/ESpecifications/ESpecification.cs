using System;
using System.Linq;
using System.Linq.Expressions;

namespace bdb.infra.specs.Specifications.ESpecifications
{
	public abstract class ESpecification<T> : ISpecification<T>
	{
		public ESpecification()
		{
			_cpFunc = new Lazy<Func<T, bool>>(InitFunc);
		}

		public abstract Expression<Func<T, bool>> TargetExpression { get; }

		public bool IsSatisfied(object entity)
		{
			if (entity.GetType() != typeof(T))
				return false;
			return IsSatisfied((T)entity);
		}

		public bool IsSatisfied(T entity)
		{
			return _cpFunc.Value(entity);
		}

		public ISpecification<T> And(ISpecification<T> specification)
		{
			return new AndESpecification<T>(this, (ESpecification<T>)specification);
		}

		public ISpecification<T> Or(ISpecification<T> specification)
		{
			return new OrESpecification<T>(this, (ESpecification<T>)specification);
		}

		public ISpecification<T> Not()
		{
			return new NotESpecification<T>(this);
		}

		public abstract ISpecification<T> RemainderUnsatisfiedBy(T entity);

		//internal abstract ISpecification[] GetComponents();

		private Lazy<Func<T, bool>> _cpFunc;

		private Func<T, bool> InitFunc()
		{
			return TargetExpression.Compile();
		}
	}
}
