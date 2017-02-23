using System;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.Tests.Mocks
{
	public sealed class SimpleAggregateRoot : IAggregateRoot
	{
		public SimpleAggregateRoot(Guid id)
		{
			this.Id = id;
		}

		public Guid Id { get; }
	}
}