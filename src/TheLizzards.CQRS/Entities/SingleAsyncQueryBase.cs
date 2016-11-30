using System.Threading.Tasks;
using TheLizzards.CQRS.Contracts;

namespace TheLizzards.CQRS.Entities
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
					DisposeResources();
				}

				disposedValue = true;
			}
		}
	}
}