using System;

namespace TheLizzards.Data.DDD
{
	public interface IAggregateRoot
	{
		Guid Id { get; }
	}
}