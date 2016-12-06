using System.Collections.Generic;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.Contracts;

namespace TheLizzards.Data.CQRS.Entities
{
	public abstract class CollectionAsyncQuery<TPayload> : IAsyncQuery<IEnumerable<TPayload>>
	{
		private bool disposedValue = false;

		public abstract Task<IEnumerable<TPayload>> Execute();

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