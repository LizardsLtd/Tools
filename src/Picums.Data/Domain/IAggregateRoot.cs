using System;

namespace Picums.Data.Domain
{
	public interface IAggregateRoot
	{
		Guid Id { get; }
	}
}