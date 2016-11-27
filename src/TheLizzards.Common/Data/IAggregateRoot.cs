using System;

namespace TheLizzards.Common.Data
{
	public interface IAggregateRoot
	{
		Guid Id { get; }
	}
}