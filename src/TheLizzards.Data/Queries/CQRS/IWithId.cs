using System;

namespace TheLizzards.Data.CQRS.Queries
{
	public interface IWithId<TResult>
	{
		TResult WithId(Guid id);
	}
}