using System;

namespace TheLizzards.Data.DDD.Contracts
{
	public interface IAggregateRoot
	{
		Guid Id { get; }
	}
}