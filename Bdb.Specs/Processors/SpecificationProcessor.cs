using Bdb.Specs.Specifications.ESpecifications;
using System.Collections.Generic;

namespace Bdb.Specs.specs.Specifications.Processors
{
	public class SpecificationProcessor
	{
		public ISpecification<TEntity>[] DecomposeFailed<TEntity>(ISpecification<TEntity> specification, TEntity onEntity)
		{
			Queue<ISpecification<TEntity>> procQueue = new Queue<ISpecification<TEntity>>();
			procQueue.Enqueue(specification);

			List<ISpecification<TEntity>> decomposed = new List<ISpecification<TEntity>>();

			while (procQueue.Count != 0)
			{
				ISpecification<TEntity> current = procQueue.Dequeue().RemainderUnsatisfiedBy(onEntity);

				if (current == null)
					continue;

				if (current is CompositeESpecification<TEntity>)
				{
					procQueue.Enqueue(((CompositeESpecification<TEntity>)current).Components[0]);
					procQueue.Enqueue(((CompositeESpecification<TEntity>)current).Components[1]);
					continue;
				}

				if (current is NotESpecification<TEntity>
					&& (((NotESpecification<TEntity>)current).SpecificationToNegate is CompositeESpecification<TEntity>
						|| ((NotESpecification<TEntity>)current).SpecificationToNegate is NotESpecification<TEntity>))
				{
					procQueue.Enqueue(current);
					continue;
				}

				decomposed.Add(current);
			}

			return decomposed.ToArray();
		}
	}
}
