using System;
using System.Linq.Expressions;

namespace TheLizzards.Data.CQRS.Queries
{
	public interface IWithSpecification<TResult, TPayload>
	{
		TResult WithSpecification(Expression<Func<TPayload, bool>> specification);
	}
}