using TheLizzards.Data.CQRS.Contracts;

namespace TheLizzards.Data.CQRS.Entities
{
	public abstract class SingleQueryBase<TPayload> : IQuery<TPayload>
	{
		public abstract TPayload Execute();

		public virtual void Dispose()
		{
		}
	}
}