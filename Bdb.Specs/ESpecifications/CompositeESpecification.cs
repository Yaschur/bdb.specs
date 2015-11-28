using System.Linq.Expressions;

namespace Bdb.Specs.Specifications.ESpecifications
{
	public abstract class CompositeESpecification<T> : ESpecification<T>
	{
		public CompositeESpecification()
		{
			Components = new ESpecification<T>[2];
		}

		//internal override ISpecification[] GetComponents()
		//{
		//	return Components;
		//}

		internal ESpecification<T>[] Components;

		protected Expression[] GetNewBodies(ParameterExpression parameter)
		{
			var expr1 = Components[0].TargetExpression;
			var expr2 = Components[1].TargetExpression;

			var leftVisitor = new ReplaceExpressionVisitor(parameter, expr1.Parameters[0]);
			var left = leftVisitor.Visit(expr1.Body);

			var rightVisitor = new ReplaceExpressionVisitor(parameter, expr2.Parameters[0]);
			var right = rightVisitor.Visit(expr2.Body);

			return new[] { left, right };
		}

		private class ReplaceExpressionVisitor : ExpressionVisitor
		{
			private readonly Expression _oldValue;
			private readonly Expression _newValue;

			public ReplaceExpressionVisitor(Expression newValue, Expression oldValue)
			{
				_newValue = newValue;
				_oldValue = oldValue;
			}

			public override Expression Visit(Expression node)
			{
				if (node == _oldValue)
					return _newValue;
				return base.Visit(node);
			}
		}
	}
}
