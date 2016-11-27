using System.Threading.Tasks;

namespace TheLizzards.CQRS
{
	public abstract class SingleAsyncQueryBase<TPayload> : IAsyncQuery<TPayload>
	{
		private bool disposedValue = false;

		public abstract Task<TPayload> Execute();

		public void Dispose() => Dispose(true);

		protected virtual void DisposeResources()
		{
		}

		private void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
					DisposeResources();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}
	}
}