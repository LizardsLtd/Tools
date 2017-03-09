using System;

namespace TheLizzards.Data.Domain
{
	public interface IAggregateRoot
	{
		Guid Id { get; }
	}
}