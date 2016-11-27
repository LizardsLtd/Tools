namespace TheLizzards.CQRS
{
	public abstract class SingleQueryBase<TPayload> : IQuery<TPayload>
	{
		public abstract TPayload Execute();

		public virtual void Dispose()
		{
		}
	}
}