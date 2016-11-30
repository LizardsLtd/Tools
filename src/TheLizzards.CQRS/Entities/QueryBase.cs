using TheLizzards.CQRS.Contracts;

namespace TheLizzards.CQRS.Entities
{
	public abstract class SingleQueryBase<TPayload> : IQuery<TPayload>
	{
		public abstract TPayload Execute();

		public virtual void Dispose()
		{
		}
	}
}