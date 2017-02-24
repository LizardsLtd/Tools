using System;
using TheLizzards.Data.CQRS.Contracts;

namespace TheLizzards.Data.CQRS.Entities
{
	[Obsolete]
	public abstract class SingleQueryBase<TPayload> : IQuery<TPayload>
	{
		public abstract TPayload Execute();

		public virtual void Dispose()
		{
		}
	}
}